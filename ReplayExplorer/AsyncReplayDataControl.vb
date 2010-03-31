Imports Tinker
Imports Strilbrary.Time
Imports Strilbrary.Values
Imports Strilbrary.Threading
Imports Strilbrary.Collections
Imports Strilbrary.Streams
Imports Tinker.WC3
Imports Tinker.WC3.Replay
Imports Tinker.Pickling
Imports System.Diagnostics.Contracts

Public Class AsyncReplayDataControl
    Private _currentFileWorkId As ModByte
    Private _currentFilterWorkId As ModByte
    Private _loadingReplay As Boolean
    Private _waitingFilter As Func(Of UInt32, ReplayEntry, Boolean)
    Public Const HeaderRowCount As Int32 = 1
    Public Const FooterRowCount As Int32 = 1

    Public Event UpdateProgress(ByVal sender As AsyncReplayDataControl, ByVal value As Int32, ByVal max As Int32, ByVal caption As String)
    Public Event FinishedLoadingReplay(ByVal sender As AsyncReplayDataControl)
    Public Event FinishedFilteringReplay(ByVal sender As AsyncReplayDataControl)

    Public Sub StartLoadingReplay(ByVal replay As ReplayReader, ByVal initialFilter As Func(Of UInt32, ReplayEntry, Boolean))
        Contract.Requires(replay IsNot Nothing)
        Contract.Requires(initialFilter IsNot Nothing)

        'Cancel any active operations
        _currentFileWorkId += CByte(1)
        _currentFilterWorkId += CByte(1)

        'Prep
        dataReplay.Rows.Clear()
        _loadingReplay = True
        _waitingFilter = Nothing

        'Start the async loading
        Dim fileWorkId = _currentFileWorkId '[hoist a local instead of the member]
        ThreadedAction(Sub() AsyncLoadReplayEntries(replay, fileWorkId, initialFilter))
    End Sub
    Public Sub StartFilteringReplay(ByVal filter As Func(Of UInt32, ReplayEntry, Boolean))
        Contract.Requires(filter IsNot Nothing)

        'Cancel any active filtering operation
        _currentFilterWorkId += CByte(1)
        _waitingFilter = Nothing

        'Wait for loading to finish before re-filtering
        If _loadingReplay Then
            _waitingFilter = filter
            Return
        End If

        'Hide and filter all the rows
        Dim rows(0 To dataReplay.RowCount - 1) As DataGridViewRow
        For i = 0 To dataReplay.RowCount - 1
            rows(i) = dataReplay.Rows(i)
        Next i
        dataReplay.Rows.Clear()

        'Begin displaying the visible rows
        ProgressiveFilterRows(_currentFilterWorkId, filter, rows, 0)
    End Sub

    Private Sub ProgressiveFilterRows(ByVal filterWorkId As ModByte,
                                      ByVal filter As Func(Of UInt32, ReplayEntry, Boolean),
                                      ByVal rows As IList(Of DataGridViewRow),
                                      ByVal nextRow As Int32)
        Contract.Requires(rows IsNot Nothing)

        'Abort if another operation has started
        If _currentFilterWorkId <> filterWorkId Then Return

        'Process for a small amount of time
        Dim t As ModInt32 = Environment.TickCount
        While nextRow < rows.Count AndAlso (Environment.TickCount() - t).UnsignedValue < 50
            Dim buffer = New List(Of DataGridViewRow)(capacity:=8196)
            While nextRow < rows.Count AndAlso buffer.Count < 8196
                buffer.Add(rows(nextRow))
                If nextRow >= HeaderRowCount AndAlso nextRow < rows.Count - FooterRowCount Then
                    Dim gameTime = DirectCast(rows(nextRow).Cells(colGameTime.Index).Value, UInt32)
                    Dim entry = DirectCast(rows(nextRow).Cells(colEntry.Index).Value, ReplayEntry)
                    rows(nextRow).Visible = filter(gameTime, entry)
                End If
                nextRow += 1
            End While
            dataReplay.Rows.AddRange(buffer.ToArray)
        End While

        'Give control back to the user for a bit before resuming work
        If nextRow = rows.Count Then
            RaiseEvent UpdateProgress(Me, 1, 1, "")
            RaiseEvent FinishedFilteringReplay(Me)
        Else
            RaiseEvent UpdateProgress(Me, nextRow, rows.Count, "Filtering...")
            Call New SystemClock().AsyncWait(100.Milliseconds).ContinueWithAction(
                Sub() Me.BeginInvoke(Sub() ProgressiveFilterRows(filterWorkId, filter, rows, nextRow)))
        End If
    End Sub

    Private Sub ProgressiveAddReplayEntries(ByVal fileWorkId As ModByte,
                                            ByVal queue As SingleConsumerLockFreeQueue(Of DataGridViewRow),
                                            ByVal parseProgress As UInteger,
                                            ByVal maxParseProgress As UInteger,
                                            ByVal lastCall As Boolean)
        Contract.Requires(queue IsNot Nothing)

        'Abort if another operation has started
        If fileWorkId <> _currentFileWorkId Then Return

        'Process for a small amount of time
        Dim t As ModInt32 = Environment.TickCount
        Dim buffer = New List(Of DataGridViewRow)(capacity:=8196)
        While Not queue.WasEmpty AndAlso (Environment.TickCount() - t).UnsignedValue < 50
            buffer.Add(queue.Dequeue)
        End While
        dataReplay.Rows.AddRange(buffer.ToArray)

        RaiseEvent UpdateProgress(Me,
                                  CInt(Math.Min(maxParseProgress, parseProgress)),
                                  CInt(maxParseProgress),
                                  "Loading Replay...")
        '[Do not self-resume work if there is still data being asynchronously added to the queue by a producer which will call us]
        If lastCall Then
            'Give control back to the user for a bit before resuming work
            If Not queue.WasEmpty Then
                Call New SystemClock().AsyncWait(100.Milliseconds).ContinueWithAction(
                    Sub() Me.BeginInvoke(Sub() ProgressiveAddReplayEntries(fileWorkId, queue, parseProgress, maxParseProgress, lastCall)))
            ElseIf _waitingFilter IsNot Nothing Then
                _loadingReplay = False
                RaiseEvent FinishedLoadingReplay(Me)
                Call StartFilteringReplay(_waitingFilter)
            Else
                _loadingReplay = False
                RaiseEvent UpdateProgress(Me, 1, 1, "")
                RaiseEvent FinishedLoadingReplay(Me)
            End If
        End If
    End Sub

    Private Sub AsyncLoadReplayEntries(ByVal replayReader As ReplayReader,
                                       ByVal fileWorkId As ModByte,
                                       ByVal initialFilter As Func(Of UInt32, ReplayEntry, Boolean))
        Contract.Requires(replayReader IsNot Nothing)
        Contract.Requires(initialFilter IsNot Nothing)

        'Abort if another operation has started
        If _currentFileWorkId <> fileWorkId Then Return

        'Prep
        Dim throttle = New Throttle(cooldown:=100.Milliseconds, clock:=New SystemClock())
        Dim queue = New SingleConsumerLockFreeQueue(Of DataGridViewRow)
        Dim totalGameTime = CUInt(replayReader.GameDuration.TotalMilliseconds)
        Dim gameTime = 0UI
        Dim sendEntriesToForm = Sub(lastCall As Boolean) Me.BeginInvoke(Sub() ProgressiveAddReplayEntries(fileWorkId, queue, gameTime, totalGameTime, lastCall))

        'Load entries
        queue.BeginEnqueue(MakeGridViewRow(True, "-", "Replay Header: " + replayReader.Description.Value))
        Try
            Dim checkTimeout = 0
            For Each entry In replayReader.Entries
                If fileWorkId <> _currentFileWorkId Then Return 'another operation was started
                If entry.Id = ReplayEntryId.Tick Then
                    Dim vals = DirectCast(entry.Payload, NamedValueMap)
                    gameTime += vals.ItemAs(Of UInt16)("time span")
                End If

                queue.BeginEnqueue(MakeGridViewRow(initialFilter(gameTime, entry), gameTime, entry))
                throttle.SetActionToRun(Sub() sendEntriesToForm(lastCall:=False))
            Next entry

            'Append finished
            queue.BeginEnqueue(MakeGridViewRow(True, gameTime, "--- finished parsing replay ---"))
        Catch ex As Exception
            queue.BeginEnqueue(MakeGridViewRow(True, gameTime, "--- error parsing replay: {0} ---".Frmt(ex)))
        End Try
        throttle.SetActionToRun(Sub() sendEntriesToForm(lastCall:=True))
    End Sub

    Private Function MakeGridViewRow(ByVal visible As Boolean, ByVal value1 As Object, ByVal value2 As Object) As DataGridViewRow
        Dim row = New DataGridViewRow()
        For Each e In {value1, value2}
            Dim cell = New DataGridViewTextBoxCell()
            cell.Value = e
            row.Cells.Add(cell)
        Next e
        row.Visible = visible
        Return row
    End Function
End Class
