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

Public Class FrmMain
    Private _loadedReplay As ReplayReader
    Private _headerReplay As ReplayReader
    Private _pristineReplayFilename As Boolean
    Private _filename As String

    Private Sub OnFormLoad() Handles Me.Load
        Me.Text = Application.ProductName
        AddHandler replayControl.dataReplay.SelectionChanged, Sub() OnGridSelectionChanged()
        AddHandler replayControl.dataReplay.CellDoubleClick, Sub() OnGridCellDoubleClick()
    End Sub
    Private Sub FrmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        replayControl.ClearExistingReplay()
    End Sub

    Private Sub OnGridCellDoubleClick() 'handles setup in OnFormLoad
        Dim cell = Me.replayControl.dataReplay.SelectedCells(0)
        Dim row = Me.replayControl.dataReplay.Rows(cell.RowIndex)
        If TypeOf row.Cells(replayControl.colEntry.Index).Value Is ReplayEntry Then
            OnClickEditEntry()
        End If
    End Sub
    Private Sub OnClickMenuFileOpen() Handles mnuOpen.Click
        If replayOpenFileDialog.ShowDialog() <> DialogResult.OK Then Return
        Me.Text = Application.ProductName + ": " + IO.Path.GetFileName(replayOpenFileDialog.FileName)

        'Clear current replay
        mnuBtnChangeTargetMap.Enabled = False
        mnuBtnImportReplayVersion.Enabled = False
        mnuSaveAs.Enabled = False
        mnuBtnSave.Enabled = False
        _loadedReplay = Nothing
        _headerReplay = Nothing
        _pristineReplayFilename = True
        replayControl.ClearExistingReplay()

        'Init replay reader
        Try
            _filename = replayOpenFileDialog.FileName
            _loadedReplay = ReplayReader.FromFile(_filename)
            _headerReplay = _loadedReplay
        Catch ex As Exception When TypeOf ex Is IO.IOException OrElse
                                   TypeOf ex Is IO.InvalidDataException OrElse
                                   TypeOf ex Is PicklingException
            _filename = Nothing
            replayControl.dataReplay.Rows.Add("Error", "Error reading replay header: {0}".Frmt(ex))
            Return
        End Try

        'Init filter control (involves reading some starting blocks)
        Try
            filterReplayControl.LoadReplay(_loadedReplay)
        Catch ex As Exception When TypeOf ex Is IO.IOException OrElse
                                   TypeOf ex Is IO.InvalidDataException OrElse
                                   TypeOf ex Is PicklingException
            replayControl.dataReplay.Rows.Add("Error", "Error reading starting replay data: {0}".Frmt(ex))
            _loadedReplay = Nothing
            _headerReplay = Nothing
            Return
        End Try

        'Start loading replay data
        mnuBtnChangeTargetMap.Enabled = True
        mnuBtnImportReplayVersion.Enabled = True
        replayControl.StartLoadingReplay(_loadedReplay, filterReplayControl.Filter())
    End Sub

    Private Sub OnProgressUpdate(ByVal sender As AsyncReplayDataControl, ByVal value As Int32, ByVal max As Int32, ByVal caption As String) Handles replayControl.UpdateProgress
        lblProgress.Visible = value < max
        pbrLoadingReplay.Visible = value < max
        lblProgress.Text = caption
        pbrLoadingReplay.Maximum = max
        pbrLoadingReplay.Value = Math.Min(value, max)
    End Sub
    Private Sub OnFinishedLoadingReplay(ByVal sender As AsyncReplayDataControl) Handles replayControl.FinishedLoadingReplay
        mnuBtnSave.Enabled = True
        mnuSaveAs.Enabled = True
    End Sub

    Private Sub OnGridSelectionChanged() 'handles setup in OnFormLoad
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
                                    rows.First.Index >= AsyncReplayDataControl.HeaderRowCount AndAlso
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
        replayControl.InsertEntryAt(row.Index,
                                    CUInt(row.Cells(0).Value),
                                    ReplayEntry.FromDefinitionAndValue(Format.ReplayEntryGameStarted, 1UI))
    End Sub
    Private Sub OnClickDeleteEntry() Handles mnuBtnDeleteSelectedEntry.Click
        Dim cell = Me.replayControl.dataReplay.SelectedCells(0)
        replayControl.DeleteEntryAt(cell.RowIndex)
    End Sub

    Private Sub OnClickImportReplayVersion() Handles mnuBtnImportReplayVersion.Click
        If replayOpenFileDialog.ShowDialog() <> DialogResult.OK Then Return
        Try
            _headerReplay = ReplayReader.FromFile(replayOpenFileDialog.FileName)
            replayControl.dataReplay(replayControl.colEntry.Index, 0).Value = _headerReplay.Description.Value
            filterReplayControl.UpdateInfo(_headerReplay.ReplayVersion)
            OnGridSelectionChanged()
        Catch ex As Exception When TypeOf ex Is IO.IOException OrElse
                                   TypeOf ex Is IO.InvalidDataException
            MessageBox.Show("Error loading replay header: {0}".Frmt(ex))
        End Try
    End Sub

    Private Sub OnClickChangeTargetMap() Handles mnuBtnChangeTargetMap.Click
        If mapOpenFileDialog.ShowDialog() <> DialogResult.OK Then Return

        'Get current values (to be replaced)
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
            'Search for warcraft 3 folder, upward from map folder
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

            'Load map info
            OnProgressUpdate(replayControl, 0, 1, "Loading Map...")
            Refresh()
            Dim map = WC3.Map.FromFile(filePath:=mapOpenFileDialog.FileName,
                                       wc3MapFolder:=curFolder.FullName,
                                       wc3PatchMPQFolder:=curFolder.Parent.FullName)
            OnProgressUpdate(replayControl, 1, 1, "")

            'Get new values
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
            Dim newEntry = ReplayEntry.FromDefinitionAndValue(Format.ReplayEntryStartOfReplay, newValue)

            'Replace old values with new values
            replayControl.dataReplay(replayControl.colEntry.Index, 1).Value = newEntry
            filterReplayControl.UpdateInfo(map.AdvertisedPath)
            OnGridSelectionChanged()
        Catch ex As Exception
            MessageBox.Show("Error loading map: {0}".Frmt(ex))
        End Try
    End Sub

    Private Sub OnClickSaveAs() Handles mnuSaveAs.Click
        If replaySaveFileDialog.ShowDialog(Me) <> DialogResult.OK Then Return
        _filename = replaySaveFileDialog.FileName
        _pristineReplayFilename = False
        OnClickSave()
    End Sub

    Private Sub OnClickSave() Handles mnuBtnSave.Click
        If _loadedReplay Is Nothing Then Return

        'Avoid accidental saves over original replay files
        If _pristineReplayFilename Then
            If MessageBox.Show("Are you sure you want to save over the original replay?",
                               "Overwrite",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Warning,
                               MessageBoxDefaultButton.Button2) <> Windows.Forms.DialogResult.Yes Then Return
            _pristineReplayFilename = False
        End If

        'Save replay data
        Using w = New ReplayWriter(stream:=New IO.FileStream(_filename, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.None).AsRandomWritableStream,
                                   settings:=_headerReplay.Settings,
                                   wc3Version:=_headerReplay.WC3Version,
                                   duration:=CUInt(_headerReplay.GameDuration.TotalMilliseconds),
                                   replayVersion:=_headerReplay.ReplayVersion)
            For i = 1 To replayControl.dataReplay.RowCount - 2
                w.WriteEntry(DirectCast(replayControl.dataReplay(replayControl.colEntry.Index, i).Value, ReplayEntry))
            Next i
        End Using

        'Update loaded replay state
        _loadedReplay = ReplayReader.FromFile(_filename)
        _headerReplay = _loadedReplay
        Me.Text = "{0}: {1}".Frmt(Application.ProductName, IO.Path.GetFileName(replaySaveFileDialog.FileName))
    End Sub
End Class
