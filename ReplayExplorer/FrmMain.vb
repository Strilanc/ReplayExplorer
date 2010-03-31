Imports Tinker
Imports Strilbrary.Time
Imports Strilbrary.Values
Imports Strilbrary.Threading
Imports Strilbrary.Collections
Imports Strilbrary.Streams
Imports Tinker.WC3
Imports Tinker.WC3.Replay
Imports Tinker.Pickling

Public Class FrmMain
    Private _loadedReplay As ReplayReader
    Private _headerReplay As ReplayReader
    Private _currentWorkId As ModByte

    Private Sub OnFormLoad() Handles Me.Load
        Me.Text = Application.ProductName
    End Sub

    Private Sub OnClickMenuFileOpen(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOpen.Click
        Try
            If replayOpenFileDialog.ShowDialog() <> DialogResult.OK Then Return
            _loadedReplay = ReplayReader.FromFile(replayOpenFileDialog.FileName)
            _headerReplay = _loadedReplay
            lblReplayVersion.Text = "Replay Version: {0}".Frmt(_headerReplay.ReplayVersion)
            lblTargetMap.Text = DirectCast(_loadedReplay.Entries.First.Payload, NamedValueMap).ItemAs(Of GameStats)("game stats").AdvertisedPath

            filterReplayControl.LoadReplay(_loadedReplay)
            Me.Text = Application.ProductName + ": " + IO.Path.GetFileName(replayOpenFileDialog.FileName)

            btnChangeTargetMap.Enabled = True
            btnImportReplayVersion.Enabled = True
            RefreshReplayView()
        Catch ex As Exception When TypeOf ex Is IO.IOException OrElse
                                   TypeOf ex Is IO.InvalidDataException OrElse
                                   TypeOf ex Is PicklingException
            MessageBox.Show("Error loading replay: {0}".Frmt(ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            _loadedReplay = Nothing
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
        mnuSaveAs.Enabled = False

        Dim replay = _loadedReplay
        Dim workId = _currentWorkId
        Dim filter = If(filterReplayControl.Visible,
                        filterReplayControl.Filter(),
                        Function(time, entry) True)
        dataReplay.Rows.Clear()
        ThreadedAction(Sub() AsyncGenerateReplayView(replay, workId, filter))
    End Sub

    Private Sub AppendRows(ByVal workId As ModByte,
                           ByVal queue As SingleConsumerLockFreeQueue(Of DataGridViewRow),
                           ByVal parseProgress As UInteger,
                           ByVal maxParseProgress As UInteger,
                           ByVal lastCall As Boolean)
        If workId <> _currentWorkId Then Return 'another operation was started
        Dim t = Environment.TickCount
        Do Until queue.WasEmpty OrElse Environment.TickCount > t + 50
            dataReplay.Rows.Add(queue.Dequeue())
        Loop

        lblProgress.Visible = True
        pbrLoadingReplay.Visible = True
        pbrLoadingReplay.Maximum = CInt(maxParseProgress \ 2)
        pbrLoadingReplay.Value = CInt(Math.Min(maxParseProgress, parseProgress) \ 2)

        If lastCall Then
            If Not queue.WasEmpty Then
                Call New SystemClock().AsyncWait(100.Milliseconds).ContinueWithAction(
                    Sub() Me.BeginInvoke(Sub() AppendRows(workId, queue, parseProgress, maxParseProgress, lastCall)))
            Else
                lblProgress.Visible = False
                pbrLoadingReplay.Visible = False
                mnuSaveAs.Enabled = mnuModeEditReplay.Checked
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
            Dim checkTimeout = 0
            For Each entry In replayReader.Entries
                If workId <> _currentWorkId Then Return 'another operation was started
                If entry.Id = ReplayEntryId.Tick Then
                    Dim vals = DirectCast(entry.Payload, NamedValueMap)
                    gameTime += vals.ItemAs(Of UInt16)("time span")
                End If

                If filter(gameTime, entry) Then
                    queue.BeginEnqueue(MakeGridViewRow(entry.Id, gameTime, entry))
                    throttle.SetActionToRun(processQueue)

                    'Allow processor to keep up
                    checkTimeout += 1
                    If checkTimeout >= 256 Then
                        checkTimeout = 0
                        While queue.Count >= 256
                            If workId <> _currentWorkId Then Return 'another operation was started
                            throttle.SetActionToRun(processQueue)
                            Threading.Thread.Sleep(10)
                        End While
                    End If
                End If
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
        Dim cells = From cell In dataReplay.SelectedCells Select DirectCast(cell, DataGridViewCell)
        Dim rows = From cell In cells
                   Select row = DirectCast(cell.OwningRow, DataGridViewRow)
                   Distinct
                   Order By row.Index

        txtDesc.Text = (From row In rows
                        Select row.Cells(2).Value
                        ).StringJoin(Environment.NewLine)
        btnEditSelectedEntry.Enabled = rows.CountUpTo(2) = 1 AndAlso
                                       TypeOf rows.Single.Cells(0).Value Is ReplayEntryId
    End Sub

    Private Sub OnFilterChange() Handles filterReplayControl.FilterChanged
        If _loadedReplay Is Nothing Then Return
        Me.BeginInvoke(Sub() RefreshReplayView())
    End Sub

    Private Sub OnClickEditEntry() Handles btnEditSelectedEntry.Click
        Dim cell = Me.dataReplay.SelectedCells(0)
        Dim row = Me.dataReplay.Rows(cell.RowIndex)
        Dim jar = New ReplayEntryJar()
        Dim pickle = jar.PackPickle(DirectCast(row.Cells(2).Value, ReplayEntry))
        Dim result = FrmEditEntry.EditEntry(Me, jar, pickle)
        If result IsNot Nothing Then
            Dim entry = DirectCast(result.Value, ReplayEntry)
            row.Cells(0).Value = entry.Id
            row.Cells(2).Value = entry
        End If
        OnGridSelectionChanged()
    End Sub

    Private Sub OnClickImportReplayVersion() Handles btnImportReplayVersion.Click
        If replayOpenFileDialog.ShowDialog() <> DialogResult.OK Then Return
        Try
            _headerReplay = ReplayReader.FromFile(replayOpenFileDialog.FileName)
            dataReplay(2, 0).Value = _headerReplay.Description.Value
            lblReplayVersion.Text = "Replay Version: {0}".Frmt(_headerReplay.ReplayVersion)
            OnGridSelectionChanged()
        Catch ex As Exception When TypeOf ex Is IO.IOException OrElse
                                   TypeOf ex Is IO.InvalidDataException
            MessageBox.Show("Error loading replay header: {0}".Frmt(ex))
        End Try
    End Sub

    Private Sub OnClickChangeTargetMap() Handles btnChangeTargetMap.Click
        If mapOpenFileDialog.ShowDialog() <> DialogResult.OK Then Return
        Try
            Dim curFolder = IO.Directory.GetParent(mapOpenFileDialog.FileName)
            Do
                If curFolder.Parent Is Nothing Then
                    Throw New InvalidOperationException("The selected file must be in a warcraft 3 maps folder.")
                ElseIf curFolder.Parent.GetFiles("War3Patch.mpq").Any() Then
                    Exit Do
                Else
                    curFolder = curFolder.Parent
                End If
            Loop

            lblTargetMap.Text = "Loading Map..."
            Refresh()
            Dim map = WC3.Map.FromFile(filePath:=mapOpenFileDialog.FileName,
                                       wc3MapFolder:=curFolder.FullName,
                                       wc3PatchMPQFolder:=curFolder.Parent.FullName)
            Dim oldEntry = DirectCast(dataReplay(2, 1).Value, ReplayEntry)
            Dim oldValue = DirectCast(oldEntry.Payload, NamedValueMap)
            Dim oldGameStats = oldValue.ItemAs(Of GameStats)("game stats")
            Dim newGameStats = GameStats.FromMapAndSettings(map,
                                                            oldGameStats.RandomHero,
                                                            oldGameStats.RandomRace,
                                                            oldGameStats.AllowFullSharedControl,
                                                            oldGameStats.LockTeams,
                                                            oldGameStats.TeamsTogether,
                                                            oldGameStats.Observers,
                                                            oldGameStats.Visibility,
                                                            oldGameStats.Speed,
                                                            oldGameStats.HostName)
            Dim newValue = oldValue.ToDictionary(Function(e) e.Key, Function(e) If(e.Key = "game stats", newGameStats, e.Value))
            Dim newEntry = ReplayEntry.FromValue(Replay.Format.ReplayEntryStartOfReplay, newValue)
            lblTargetMap.Text = newGameStats.AdvertisedPath
            dataReplay(2, 1).Value = newEntry
            OnGridSelectionChanged()
        Catch ex As Exception
            MessageBox.Show("Error loading map: {0}".Frmt(ex))
        End Try
    End Sub

    Private Sub OnClickModeExploreReplay() Handles mnuModeExploreReplay.Click
        mnuModeEditReplay.Checked = False
        mnuModeExploreReplay.Checked = True
        panelEditControls.Visible = False
        filterReplayControl.Visible = True
        RefreshReplayView()
    End Sub
    Private Sub OnClickModeEditReplay() Handles mnuModeEditReplay.Click
        mnuModeEditReplay.Checked = True
        mnuModeExploreReplay.Checked = False
        panelEditControls.Visible = True
        filterReplayControl.Visible = False
        RefreshReplayView()
    End Sub

    Private Sub OnClickSaveAs() Handles mnuSaveAs.Click
        If replaySaveFileDialog.ShowDialog(Me) <> DialogResult.OK Then Return
        Using w = New ReplayWriter(stream:=New IO.FileStream(replaySaveFileDialog.FileName, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.None).AsRandomWritableStream,
                                   settings:=_headerReplay.Settings,
                                   wc3Version:=_headerReplay.WC3Version,
                                   duration:=CUInt(_headerReplay.GameDuration.TotalMilliseconds),
                                   replayVersion:=_headerReplay.ReplayVersion)
            For i = 1 To dataReplay.RowCount - 2
                w.WriteEntry(DirectCast(dataReplay(2, i).Value, ReplayEntry))
            Next i
        End Using

        _loadedReplay = ReplayReader.FromFile(replaySaveFileDialog.FileName)
        _headerReplay = _loadedReplay
        Me.Text = Application.ProductName + ": " + IO.Path.GetFileName(replaySaveFileDialog.FileName)
    End Sub
End Class
