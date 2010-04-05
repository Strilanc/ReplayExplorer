<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMain))
        Me.mnuMain = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOpen = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSaveAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBtnImportReplayVersion = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBtnChangeTargetMap = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBtnEditSelectedEntry = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBtnDeleteSelectedEntry = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBtnInsertEntry = New System.Windows.Forms.ToolStripMenuItem()
        Me.replayOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.lscActionTypeFilter = New System.Windows.Forms.CheckedListBox()
        Me.chkSkipEmptyTicks = New System.Windows.Forms.CheckBox()
        Me.pbrLoadingReplay = New System.Windows.Forms.ProgressBar()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.mapOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.replaySaveFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.replayControl = New ReplayExplorer.AsyncReplayDataControl()
        Me.filterReplayControl = New ReplayExplorer.FilterControl()
        Me.mnuBtnSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'mnuMain
        '
        Me.mnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.EditToolStripMenuItem1})
        Me.mnuMain.Location = New System.Drawing.Point(0, 0)
        Me.mnuMain.Name = "mnuMain"
        Me.mnuMain.Size = New System.Drawing.Size(924, 24)
        Me.mnuMain.TabIndex = 10
        Me.mnuMain.Text = "MenuStrip1"
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuOpen, Me.mnuBtnSave, Me.mnuSaveAs})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(37, 20)
        Me.mnuFile.Text = "File"
        '
        'mnuOpen
        '
        Me.mnuOpen.Name = "mnuOpen"
        Me.mnuOpen.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.mnuOpen.Size = New System.Drawing.Size(198, 22)
        Me.mnuOpen.Text = "Open"
        '
        'mnuSaveAs
        '
        Me.mnuSaveAs.Enabled = False
        Me.mnuSaveAs.Name = "mnuSaveAs"
        Me.mnuSaveAs.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.mnuSaveAs.Size = New System.Drawing.Size(198, 22)
        Me.mnuSaveAs.Text = "Save As ..."
        '
        'EditToolStripMenuItem1
        '
        Me.EditToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuBtnImportReplayVersion, Me.mnuBtnChangeTargetMap, Me.ToolStripSeparator1, Me.mnuBtnEditSelectedEntry, Me.mnuBtnDeleteSelectedEntry, Me.mnuBtnInsertEntry})
        Me.EditToolStripMenuItem1.Name = "EditToolStripMenuItem1"
        Me.EditToolStripMenuItem1.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem1.Text = "Edit"
        '
        'mnuBtnImportReplayVersion
        '
        Me.mnuBtnImportReplayVersion.Name = "mnuBtnImportReplayVersion"
        Me.mnuBtnImportReplayVersion.Size = New System.Drawing.Size(290, 22)
        Me.mnuBtnImportReplayVersion.Text = "Import Replay Version"
        Me.mnuBtnImportReplayVersion.ToolTipText = "Allows you to update the replay's version, by importing it from another replay." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & _
            "(Can make old replays compatible, but the game may play out differently.)"
        '
        'mnuBtnChangeTargetMap
        '
        Me.mnuBtnChangeTargetMap.Name = "mnuBtnChangeTargetMap"
        Me.mnuBtnChangeTargetMap.Size = New System.Drawing.Size(290, 22)
        Me.mnuBtnChangeTargetMap.Text = "Change Target Map"
        Me.mnuBtnChangeTargetMap.ToolTipText = "Allows you to select the map the replay says it applies to." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(The new map should " & _
            "be extremely similar to the true map.)"
        '
        'mnuBtnEditSelectedEntry
        '
        Me.mnuBtnEditSelectedEntry.Name = "mnuBtnEditSelectedEntry"
        Me.mnuBtnEditSelectedEntry.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.mnuBtnEditSelectedEntry.Size = New System.Drawing.Size(290, 22)
        Me.mnuBtnEditSelectedEntry.Text = "Edit Selected Entry"
        Me.mnuBtnEditSelectedEntry.ToolTipText = "Allows you to edit the raw data making up a replay entry."
        '
        'mnuBtnDeleteSelectedEntry
        '
        Me.mnuBtnDeleteSelectedEntry.Name = "mnuBtnDeleteSelectedEntry"
        Me.mnuBtnDeleteSelectedEntry.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Delete), System.Windows.Forms.Keys)
        Me.mnuBtnDeleteSelectedEntry.Size = New System.Drawing.Size(290, 22)
        Me.mnuBtnDeleteSelectedEntry.Text = "Delete Selected Entry"
        '
        'mnuBtnInsertEntry
        '
        Me.mnuBtnInsertEntry.Name = "mnuBtnInsertEntry"
        Me.mnuBtnInsertEntry.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Insert), System.Windows.Forms.Keys)
        Me.mnuBtnInsertEntry.Size = New System.Drawing.Size(290, 22)
        Me.mnuBtnInsertEntry.Text = "Insert Entry At Selected Position"
        '
        'replayOpenFileDialog
        '
        Me.replayOpenFileDialog.Filter = "Warcraft 3 Replay Files (*.w3g)|*.w3g"
        '
        'txtDesc
        '
        Me.txtDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDesc.Location = New System.Drawing.Point(426, 413)
        Me.txtDesc.Multiline = True
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDesc.Size = New System.Drawing.Size(498, 199)
        Me.txtDesc.TabIndex = 11
        Me.txtDesc.WordWrap = False
        '
        'lscActionTypeFilter
        '
        Me.lscActionTypeFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lscActionTypeFilter.FormattingEnabled = True
        Me.lscActionTypeFilter.HorizontalScrollbar = True
        Me.lscActionTypeFilter.Location = New System.Drawing.Point(133, 296)
        Me.lscActionTypeFilter.Name = "lscActionTypeFilter"
        Me.lscActionTypeFilter.Size = New System.Drawing.Size(129, 184)
        Me.lscActionTypeFilter.TabIndex = 20
        '
        'chkSkipEmptyTicks
        '
        Me.chkSkipEmptyTicks.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSkipEmptyTicks.AutoSize = True
        Me.chkSkipEmptyTicks.Location = New System.Drawing.Point(12, 362)
        Me.chkSkipEmptyTicks.Name = "chkSkipEmptyTicks"
        Me.chkSkipEmptyTicks.Size = New System.Drawing.Size(109, 17)
        Me.chkSkipEmptyTicks.TabIndex = 13
        Me.chkSkipEmptyTicks.Text = "Ignore empty ticks"
        Me.chkSkipEmptyTicks.UseVisualStyleBackColor = True
        '
        'pbrLoadingReplay
        '
        Me.pbrLoadingReplay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbrLoadingReplay.Location = New System.Drawing.Point(622, 4)
        Me.pbrLoadingReplay.Name = "pbrLoadingReplay"
        Me.pbrLoadingReplay.Size = New System.Drawing.Size(290, 20)
        Me.pbrLoadingReplay.TabIndex = 21
        Me.pbrLoadingReplay.Visible = False
        '
        'lblProgress
        '
        Me.lblProgress.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblProgress.Location = New System.Drawing.Point(526, 9)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(90, 13)
        Me.lblProgress.TabIndex = 22
        Me.lblProgress.Text = "Loading Replay..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblProgress.Visible = False
        '
        'mapOpenFileDialog
        '
        Me.mapOpenFileDialog.Filter = "WC3 Map Files (*.w3m;*.w3x)|*.w3m;*.w3x"
        '
        'replaySaveFileDialog
        '
        Me.replaySaveFileDialog.Filter = "Warcraft 3 Replays (*.w3g)|*.w3g"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(287, 6)
        '
        'replayControl
        '
        Me.replayControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.replayControl.Location = New System.Drawing.Point(0, 27)
        Me.replayControl.Name = "replayControl"
        Me.replayControl.Size = New System.Drawing.Size(924, 383)
        Me.replayControl.TabIndex = 25
        '
        'filterReplayControl
        '
        Me.filterReplayControl.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.filterReplayControl.AutoScroll = True
        Me.filterReplayControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.filterReplayControl.Location = New System.Drawing.Point(0, 413)
        Me.filterReplayControl.Name = "filterReplayControl"
        Me.filterReplayControl.Size = New System.Drawing.Size(420, 199)
        Me.filterReplayControl.TabIndex = 23
        '
        'mnuBtnSave
        '
        Me.mnuBtnSave.Name = "mnuBtnSave"
        Me.mnuBtnSave.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.mnuBtnSave.Size = New System.Drawing.Size(198, 22)
        Me.mnuBtnSave.Text = "Save"
        '
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(924, 613)
        Me.Controls.Add(Me.replayControl)
        Me.Controls.Add(Me.filterReplayControl)
        Me.Controls.Add(Me.pbrLoadingReplay)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.mnuMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.mnuMain
        Me.Name = "FrmMain"
        Me.Text = "{ProductName}"
        Me.mnuMain.ResumeLayout(False)
        Me.mnuMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents mnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuOpen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents replayOpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents lscActionTypeFilter As System.Windows.Forms.CheckedListBox
    Friend WithEvents chkSkipEmptyTicks As System.Windows.Forms.CheckBox
    Friend WithEvents pbrLoadingReplay As System.Windows.Forms.ProgressBar
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents mnuSaveAs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents filterReplayControl As ReplayExplorer.FilterControl
    Friend WithEvents mapOpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents replaySaveFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents replayControl As ReplayExplorer.AsyncReplayDataControl
    Friend WithEvents EditToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBtnImportReplayVersion As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBtnChangeTargetMap As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBtnEditSelectedEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBtnDeleteSelectedEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBtnInsertEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuBtnSave As System.Windows.Forms.ToolStripMenuItem

End Class
