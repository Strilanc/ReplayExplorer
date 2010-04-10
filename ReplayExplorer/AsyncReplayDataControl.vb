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
    Private _rows As IList(Of DataGridViewRow)
    Public Const HeaderRowCount As Int32 = 1
    Public Const FooterRowCount As Int32 = 1

    Public Event UpdateProgress(ByVal sender As AsyncReplayDataControl, ByVal value As Int32, ByVal max As Int32, ByVal caption As String)
    Public Event FinishedLoadingReplay(ByVal sender As AsyncReplayDataControl)
    Public Event FinishedFilteringReplay(ByVal sender As AsyncReplayDataControl)

    Public Sub ClearExistingReplay()
        _currentFileWorkId += CByte(1)
        _currentFilterWorkId += CByte(1)
        dataReplay.Rows.Clear()
        _waitingFilter = Nothing
        _loadingReplay = False
    End Sub
    Public Sub StartLoadingReplay(ByVal replay As ReplayReader, ByVal initialFilter As Func(Of UInt32, ReplayEntry, Boolean))
        Contract.Requires(replay IsNot Nothing)
        Contract.Requires(initialFilter IsNot Nothing)

        ClearExistingReplay()

        'Start the async loading
        _loadingReplay = True
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

        'Hide all rows then progressivly show desired rows
        dataReplay.Rows.Clear()
        ProgressiveFilterRows(_currentFilterWorkId, filter, 0)
    End Sub

    Public Sub DeleteEntryAt(ByVal index As Int32)
        dataReplay.Rows.RemoveAt(index)
        _rows.RemoveAt(index)
    End Sub
    Public Sub InsertEntryAt(ByVal index As Int32, ByVal time As UInt32, ByVal entry As ReplayEntry)
        Contract.Requires(entry IsNot Nothing)
        dataReplay.Rows.Insert(index, time, entry)
        _rows.Insert(index, dataReplay.Rows(index))
    End Sub

    Private Sub ProgressiveFilterRows(ByVal filterWorkId As ModByte,
                                      ByVal filter As Func(Of UInt32, ReplayEntry, Boolean),
                                      ByVal nextRow As Int32)

        'Abort if another operation has started
        If _currentFilterWorkId <> filterWorkId Then Return

        'Process for a small amount of time
        Dim t As ModInt32 = Environment.TickCount
        While nextRow < _rows.Count AndAlso (Environment.TickCount() - t).UnsignedValue < 25
            Dim buffer = New List(Of DataGridViewRow)(capacity:=8196)
            While nextRow < _rows.Count AndAlso buffer.Count < 8196
                buffer.Add(_rows(nextRow))
                If nextRow >= HeaderRowCount AndAlso nextRow < _rows.Count - FooterRowCount Then
                    Dim gameTime = DirectCast(_rows(nextRow).Cells(colGameTime.Index).Value, UInt32)
                    Dim entry = DirectCast(_rows(nextRow).Cells(colEntry.Index).Value, ReplayEntry)
                    _rows(nextRow).Visible = filter(gameTime, entry)
                End If
                nextRow += 1
            End While
            dataReplay.Rows.AddRange(buffer.ToArray)
        End While

        'Give control back to the user for a bit before resuming work
        If nextRow = _rows.Count Then
            RaiseEvent UpdateProgress(Me, 1, 1, "")
            RaiseEvent FinishedFilteringReplay(Me)
        Else
            RaiseEvent UpdateProgress(Me, nextRow, _rows.Count, "Filtering...")
            Call New SystemClock().AsyncWait(100.Milliseconds).ContinueWithAction(
                Sub() Me.BeginInvoke(Sub() ProgressiveFilterRows(filterWorkId, filter, nextRow)))
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
        While Not queue.WasEmpty AndAlso (Environment.TickCount() - t).UnsignedValue < 25
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
            Else
                'Copy rows
                _rows = New List(Of DataGridViewRow)
                For i = 0 To dataReplay.RowCount - 1
                    _rows.Add(dataReplay.Rows(i))
                Next i

                'Done loading
                _loadingReplay = False
                If _waitingFilter Is Nothing Then
                    RaiseEvent UpdateProgress(Me, 1, 1, "")
                End If
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
                If entry.Definition Is Format.ReplayEntryTick Then
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
