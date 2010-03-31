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

    Private Sub OnFormLoad() Handles Me.Load
        Me.Text = Application.ProductName
        AddHandler replayControl.dataReplay.SelectionChanged, Sub() OnGridSelectionChanged()
        AddHandler replayControl.dataReplay.CellDoubleClick, AddressOf OnGridCellDoubleClick
    End Sub

    Private Sub OnGridCellDoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)
        Dim cell = Me.replayControl.dataReplay.SelectedCells(0)
        Dim row = Me.replayControl.dataReplay.Rows(cell.RowIndex)
        If TypeOf row.Cells(replayControl.colEntry.Index).Value Is ReplayEntry Then
            OnClickEditEntry()
        End If
    End Sub
    Private Sub OnClickMenuFileOpen(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOpen.Click
        Try
            If replayOpenFileDialog.ShowDialog() <> DialogResult.OK Then Return
            _loadedReplay = ReplayReader.FromFile(replayOpenFileDialog.FileName)
            _headerReplay = _loadedReplay
            'lblReplayVersion.Text = "Replay Version: {0}".Frmt(_headerReplay.ReplayVersion)
            'lblTargetMap.Text = DirectCast(_loadedReplay.Entries.First.Payload, NamedValueMap).ItemAs(Of GameStats)("game stats").AdvertisedPath

            filterReplayControl.LoadReplay(_loadedReplay)
            Me.Text = Application.ProductName + ": " + IO.Path.GetFileName(replayOpenFileDialog.FileName)

            mnuBtnChangeTargetMap.Enabled = True
            mnuBtnImportReplayVersion.Enabled = True

            replayControl.StartLoadingReplay(_loadedReplay, filterReplayControl.Filter())
        Catch ex As Exception When TypeOf ex Is IO.IOException OrElse
                                   TypeOf ex Is IO.InvalidDataException OrElse
                                   TypeOf ex Is PicklingException
            MessageBox.Show("Error loading replay: {0}".Frmt(ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            _loadedReplay = Nothing
        End Try
    End Sub

    Private Sub OnProgressUpdate(ByVal sender As AsyncReplayDataControl, ByVal value As Int32, ByVal max As Int32, ByVal caption As String) Handles replayControl.UpdateProgress
        lblProgress.Visible = value < max
        pbrLoadingReplay.Visible = value < max
        lblProgress.Text = caption
        pbrLoadingReplay.Maximum = max
        pbrLoadingReplay.Value = Math.Min(value, max)
    End Sub
    Private Sub OnFinishedLoadingReplay(ByVal sender As AsyncReplayDataControl) Handles replayControl.FinishedLoadingReplay
        mnuSaveAs.Enabled = True
    End Sub
    Private Sub OnFinishedFilteringReplay(ByVal sender As AsyncReplayDataControl) Handles replayControl.FinishedFilteringReplay

    End Sub

    Private Sub OnGridSelectionChanged()
        Dim cells = From cell In replayControl.dataReplay.SelectedCells Select DirectCast(cell, DataGridViewCell)
        Dim rows = From cell In cells
                   Select row = DirectCast(cell.OwningRow, DataGridViewRow)
                   Distinct
                   Order By row.Index

        txtDesc.Text = (From row In rows
                        Select row.Cells(replayControl.colEntry.Index).Value
                        ).StringJoin(Environment.NewLine)
        mnuBtnEditSelectedEntry.Enabled = rows.CountUpTo(2) = 1 AndAlso
                                          TypeOf rows.Single.Cells(replayControl.colEntry.Index).Value Is ReplayEntry
        mnuBtnDeleteSelectedEntry.Enabled = mnuBtnEditSelectedEntry.Enabled
        mnuBtnInsertEntry.Enabled = rows.CountUpTo(2) = 1 AndAlso
                                    rows.First.Index > AsyncReplayDataControl.HeaderRowCount AndAlso
                                    rows.First.Index <= replayControl.dataReplay.RowCount - AsyncReplayDataControl.HeaderRowCount
    End Sub

    Private Sub OnFilterChange() Handles filterReplayControl.FilterChanged
        If _loadedReplay Is Nothing Then Return
        replayControl.StartFilteringReplay(filterReplayControl.Filter())
    End Sub

    Private Sub OnClickEditEntry() Handles mnuBtnEditSelectedEntry.Click
        Dim cell = Me.replayControl.dataReplay.SelectedCells(0)
        Dim row = Me.replayControl.dataReplay.Rows(cell.RowIndex)
        Dim jar = New ReplayEntryJar()
        Dim pickle = jar.PackPickle(DirectCast(row.Cells(replayControl.colEntry.Index).Value, ReplayEntry))
        Dim result = FrmEditEntry.EditEntry(Me, jar, pickle)
        If result IsNot Nothing Then
            row.Cells(replayControl.colEntry.Index).Value = DirectCast(result.Value, ReplayEntry)
        End If
        OnGridSelectionChanged()
    End Sub
    Private Sub OnClickInsertEntry() Handles mnuBtnInsertEntry.Click
        Dim cell = Me.replayControl.dataReplay.SelectedCells(0)
        Dim row = Me.replayControl.dataReplay.Rows(cell.RowIndex)
        replayControl.dataReplay.Rows.Insert(row.Index, row.Cells(0).Value, ReplayEntry.FromValue(Replay.Format.ReplayEntryGameStarted, 1UI))
    End Sub
    Private Sub OnClickDeleteEntry() Handles mnuBtnDeleteSelectedEntry.Click
        Dim cell = Me.replayControl.dataReplay.SelectedCells(0)
        Dim row = Me.replayControl.dataReplay.Rows(cell.RowIndex)
        replayControl.dataReplay.Rows.RemoveAt(row.Index)
    End Sub

    Private Sub OnClickImportReplayVersion() Handles mnuBtnImportReplayVersion.Click
        If replayOpenFileDialog.ShowDialog() <> DialogResult.OK Then Return
        Try
            _headerReplay = ReplayReader.FromFile(replayOpenFileDialog.FileName)
            replayControl.dataReplay(replayControl.colEntry.Index, 0).Value = _headerReplay.Description.Value
            'lblReplayVersion.Text = "Replay Version: {0}".Frmt(_headerReplay.ReplayVersion)
            OnGridSelectionChanged()
        Catch ex As Exception When TypeOf ex Is IO.IOException OrElse
                                   TypeOf ex Is IO.InvalidDataException
            MessageBox.Show("Error loading replay header: {0}".Frmt(ex))
        End Try
    End Sub

    Private Sub OnClickChangeTargetMap() Handles mnuBtnChangeTargetMap.Click
        If mapOpenFileDialog.ShowDialog() <> DialogResult.OK Then Return
        Dim oldValue As NamedValueMap
        Dim oldGameStats As GameStats
        Try
            Dim oldEntry = DirectCast(replayControl.dataReplay(replayControl.colEntry.Index, 1).Value, ReplayEntry)
            oldValue = DirectCast(oldEntry.Payload, NamedValueMap)
            oldGameStats = oldValue.ItemAs(Of GameStats)("game stats")
        Catch ex As InvalidCastException
            MessageBox.Show("Error finding StartOfReplay's game stats entry: {0}".Frmt(ex))
            Return
        End Try

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

            OnProgressUpdate(replayControl, 0, 1, "Loading Map...")
            Refresh()
            Dim map = WC3.Map.FromFile(filePath:=mapOpenFileDialog.FileName,
                                       wc3MapFolder:=curFolder.FullName,
                                       wc3PatchMPQFolder:=curFolder.Parent.FullName)
            OnProgressUpdate(replayControl, 1, 1, "")
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
            'lblTargetMap.Text = newGameStats.AdvertisedPath
            replayControl.dataReplay(replayControl.colEntry.Index, 1).Value = newEntry
            OnGridSelectionChanged()
        Catch ex As Exception
            MessageBox.Show("Error loading map: {0}".Frmt(ex))
        End Try
    End Sub

    Private Sub OnClickSaveAs() Handles mnuSaveAs.Click
        If replaySaveFileDialog.ShowDialog(Me) <> DialogResult.OK Then Return
        Using w = New ReplayWriter(stream:=New IO.FileStream(replaySaveFileDialog.FileName, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.None).AsRandomWritableStream,
                                   settings:=_headerReplay.Settings,
                                   wc3Version:=_headerReplay.WC3Version,
                                   duration:=CUInt(_headerReplay.GameDuration.TotalMilliseconds),
                                   replayVersion:=_headerReplay.ReplayVersion)
            For i = 1 To replayControl.dataReplay.RowCount - 2
                w.WriteEntry(DirectCast(replayControl.dataReplay(replayControl.colEntry.Index, i).Value, ReplayEntry))
            Next i
        End Using

        _loadedReplay = ReplayReader.FromFile(replaySaveFileDialog.FileName)
        _headerReplay = _loadedReplay
        Me.Text = Application.ProductName + ": " + IO.Path.GetFileName(replaySaveFileDialog.FileName)
    End Sub
End Class
