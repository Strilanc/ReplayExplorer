Imports Tinker
Imports Strilbrary.Time
Imports Strilbrary.Values
Imports Strilbrary.Threading
Imports Strilbrary.Collections
Imports Tinker.WC3.Replay

Public Class FrmMain
    Private _loadedReplay As ReplayReader
    Private _currentWorkId As ModByte
    Private _ignoreEvents As Boolean

    Private Sub OnFormLoad() Handles Me.Load
        Me.Text = Application.ProductName
        For Each e In (From v In EnumValues(Of WC3.Protocol.GameActionId)() Order By v.ToString)
            lscActionTypes.Items.Add(e, isChecked:=True)
        Next e
        For Each e In (From v In EnumValues(Of ReplayEntryId)() Order By v.ToString)
            lscEntryTypeFilter.Items.Add(e, isChecked:=e <> ReplayEntryId.GameStateChecksum)
        Next e
    End Sub

    Private Sub OnClickMenuFileOpen(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOpen.Click
        Try
            replayOpenFileDialog.Filter = "Warcraft 3 Replays (*.w3g)|*.w3g"
            If replayOpenFileDialog.ShowDialog() <> DialogResult.OK Then Return
            _loadedReplay = ReplayReader.FromFile(replayOpenFileDialog.FileName)

            'Populate player list
            _ignoreEvents = True
            lscFilterPlayers.Items.Clear()
            Me.Text = Application.ProductName + ": " + IO.Path.GetFileName(replayOpenFileDialog.FileName)
            For Each entry In _loadedReplay.Entries
                Dim name As InvariantString
                Dim pid As WC3.PlayerId
                Select Case entry.Id
                    Case ReplayEntryId.PlayerJoined
                        Dim vals = DirectCast(entry.Payload.Value, NamedValueMap)
                        name = vals.ItemAs(Of String)("name")
                        pid = vals.ItemAs(Of WC3.PlayerId)("joiner id")
                    Case ReplayEntryId.StartOfReplay
                        Dim vals = DirectCast(entry.Payload.Value, NamedValueMap)
                        name = vals.ItemAs(Of String)("primary player name")
                        pid = vals.ItemAs(Of WC3.PlayerId)("primary player id")
                    Case Else
                        Exit For
                End Select
                If lscFilterPlayers.Items.Count > 12 Then Throw New IO.InvalidDataException("Replay has too many player entries.")
                lscFilterPlayers.Items.Add("{0}: {1}".Frmt(pid.Index, name), True)
            Next entry

            RefreshReplayView()
        Catch ex As Exception When TypeOf ex Is IO.IOException OrElse
                                   TypeOf ex Is IO.InvalidDataException
            MessageBox.Show("Error loading replay: {0}".Frmt(ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            _loadedReplay = Nothing
        Finally
            _ignoreEvents = False
        End Try
    End Sub

    Private Function TryParseGameTime(ByVal text As String) As UInt32?
        If text Is Nothing Then Return Nothing
        If UInt32.TryParse(text, 0) Then Return UInt32.Parse(text)
        If TimeSpan.TryParse(text, Nothing) Then
            Dim d = TimeSpan.Parse(text).TotalMilliseconds
            If d < 0 Then Return 0UI
            If d > UInt32.MaxValue Then Return UInt32.MaxValue
            Return CUInt(d)
        End If
        Return Nothing
    End Function

    Private Sub RefreshReplayView()
        If _loadedReplay Is Nothing Then Return
        _currentWorkId += CByte(1)

        dataReplay.Rows.Clear()

        Dim startTime = TryParseGameTime(txtMinGameTime.Text)
        Dim endTime = TryParseGameTime(txtMaxGameTime.Text)
        Dim timeFilter = Function(time As UInt32, entry As ReplayEntry)
                             If startTime IsNot Nothing AndAlso time < startTime Then Return False
                             If endTime IsNot Nothing AndAlso time > endTime Then Return False
                             Return True
                         End Function

        Dim playerPids = (From item In lscFilterPlayers.CheckedItems Select Byte.Parse(item.ToString.Split(":"c)(0))).ToArray
        Dim playerFilter = Function(time As UInt32, entry As ReplayEntry)
                               If entry.Id <> ReplayEntryId.Tick Then Return True
                               Dim vals = DirectCast(entry.Payload.Value, NamedValueMap)
                               Dim actions = vals.ItemAs(Of IReadableList(Of Tinker.WC3.Protocol.PlayerActionSet))("player action sets")
                               If actions.Count <= 0 Then Return True
                               Return (From action In actions Where playerPids.Contains(action.Id.Index)).Any
                           End Function

        Dim entryTypes = (From item In lscEntryTypeFilter.CheckedItems Select CType(item, ReplayEntryId)).ToArray
        Dim entryFilter = Function(time As UInt32, entry As ReplayEntry)
                              Return entryTypes.Contains(entry.Id)
                          End Function

        Dim showEmptyActions = Not chkIgnoreEmptyTicks.Checked
        Dim actionTypes = (From item In lscActionTypes.CheckedItems Select CType(item, WC3.Protocol.GameActionId)).ToArray
        Dim actionFilter = Function(time As UInt32, entry As ReplayEntry)
                               If entry.Id <> ReplayEntryId.Tick Then Return True
                               Dim vals = DirectCast(entry.Payload.Value, NamedValueMap)
                               Dim actions = vals.ItemAs(Of IReadableList(Of Tinker.WC3.Protocol.PlayerActionSet))("player action sets")
                               If actions.Count <= 0 Then Return showEmptyActions
                               Return (From actionSet In actions
                                       From action In actionSet.Actions
                                       Where actionTypes.Contains(action.Id)).Any
                           End Function

        ThreadedAction(Sub() AsyncGenerateReplayView(_loadedReplay,
                                                     _currentWorkId,
                                                     Function(e1, e2) playerFilter(e1, e2) AndAlso
                                                                      entryFilter(e1, e2) AndAlso
                                                                      timeFilter(e1, e2) AndAlso
                                                                      actionFilter(e1, e2)))
    End Sub

    Private Sub AppendRows(ByVal workId As ModByte,
                           ByVal queue As SingleConsumerLockFreeQueue(Of DataGridViewRow),
                           ByVal parseProgress As UInteger,
                           ByVal maxProgress As UInteger,
                           ByVal lastCall As Boolean)
        If workId <> _currentWorkId Then Return 'another operation was started
        Dim t = Environment.TickCount
        Do Until queue.WasEmpty OrElse Environment.TickCount > t + 50
            dataReplay.Rows.Add(queue.Dequeue())
        Loop

        lblProgress.Visible = True
        pbrLoadingReplay.Visible = True
        pbrLoadingReplay.Maximum = CInt(maxProgress \ 2)
        pbrLoadingReplay.Value = CInt(Math.Min(maxProgress, parseProgress) \ 2)

        If lastCall Then
            If Not queue.WasEmpty Then
                Call New SystemClock().AsyncWait(100.Milliseconds).ContinueWithAction(
                    Sub() Me.BeginInvoke(Sub() AppendRows(workId, queue, parseProgress, maxProgress, lastCall)))
            Else
                lblProgress.Visible = False
                pbrLoadingReplay.Visible = False
            End If
        End If
    End Sub
    Private Sub AsyncGenerateReplayView(ByVal replayReader As ReplayReader,
                                        ByVal workId As ModByte,
                                        ByVal filter As Func(Of UInt32, ReplayEntry, Boolean))
        Dim throttle = New Throttle(cooldown:=100.Milliseconds, clock:=New SystemClock())
        Dim queue = New SingleConsumerLockFreeQueue(Of DataGridViewRow)
        Dim totalGameTime = CUInt(replayReader.GameDuration.TotalMilliseconds)
        Dim gameTime = 0UI
        Dim processQueue = Sub() Me.BeginInvoke(Sub() AppendRows(workId, queue, gameTime, totalGameTime, False))
        Dim flushQueue = Sub() Me.BeginInvoke(Sub() AppendRows(workId, queue, gameTime, totalGameTime, True))

        queue.BeginEnqueue(MakeGridViewRow("Replay Header", "-", replayReader.Description.Value))
        Try
            For Each entry In replayReader.Entries
                If workId <> _currentWorkId Then Return 'another operation was started
                If entry.Id = ReplayEntryId.Tick Then
                    Dim vals = DirectCast(entry.Payload.Value, NamedValueMap)
                    gameTime += vals.ItemAs(Of UInt16)("time span")
                End If

                If filter(gameTime, entry) Then
                    queue.BeginEnqueue(MakeGridViewRow(entry.Id, gameTime, entry.Payload))
                End If
                throttle.SetActionToRun(processQueue)
            Next entry

            'Append finished
            queue.BeginEnqueue(MakeGridViewRow("-", gameTime, "--- finished parsing replay ---"))
        Catch ex As Exception
            queue.BeginEnqueue(MakeGridViewRow("-", gameTime, "--- error parsing replay: {0} ---".Frmt(ex)))
        End Try
        throttle.SetActionToRun(flushQueue)
    End Sub
    Private Function MakeGridViewRow(ByVal ParamArray vals() As Object) As DataGridViewRow
        Dim row = New DataGridViewRow()
        For Each e In vals
            Dim cell = New DataGridViewTextBoxCell()
            cell.Value = e
            row.Cells.Add(cell)
        Next e
        Return row
    End Function

    Private Sub OnGridSelectionChanged() Handles dataReplay.SelectionChanged
        txtDesc.Text = (From cell In dataReplay.SelectedCells
                        Distinct Select row = CType(cell, DataGridViewCell).OwningRow
                        Select CType(row, DataGridViewRow).Cells(2).Value).StringJoin(Environment.NewLine)
    End Sub

    Private Sub OnFilterChange() Handles txtMinGameTime.TextChanged,
                                         txtMaxGameTime.TextChanged,
                                         chkIgnoreEmptyTicks.CheckedChanged,
                                         lscEntryTypeFilter.ItemCheck,
                                         lscFilterPlayers.ItemCheck,
                                         lscActionTypes.ItemCheck
        If _ignoreEvents OrElse _loadedReplay Is Nothing Then Return
        Me.BeginInvoke(Sub() RefreshReplayView())
    End Sub

    Private Sub OnClickAllActionTypes() Handles btnAllActionTypes.Click
        Try
            _ignoreEvents = True
            For i = 0 To lscActionTypes.Items.Count - 1
                lscActionTypes.SetItemChecked(i, True)
            Next i
            RefreshReplayView()
        Finally
            _ignoreEvents = False
        End Try
    End Sub
    Private Sub OnClickNoActionTypes() Handles btnNoActionTypes.Click
        Try
            _ignoreEvents = True
            For i = 0 To lscActionTypes.Items.Count - 1
                lscActionTypes.SetItemChecked(i, False)
            Next i
            RefreshReplayView()
        Finally
            _ignoreEvents = False
        End Try
    End Sub
End Class
