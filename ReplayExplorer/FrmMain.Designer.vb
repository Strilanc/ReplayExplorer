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
        Me.dataReplay = New System.Windows.Forms.DataGridView()
        Me.colEntryType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colGameTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mnuMain = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOpen = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSaveAs = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuModeExploreReplay = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuModeEditReplay = New System.Windows.Forms.ToolStripMenuItem()
        Me.replayOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.lscActionTypeFilter = New System.Windows.Forms.CheckedListBox()
        Me.chkSkipEmptyTicks = New System.Windows.Forms.CheckBox()
        Me.pbrLoadingReplay = New System.Windows.Forms.ProgressBar()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.panelEditControls = New System.Windows.Forms.Panel()
        Me.lblTargetMap = New System.Windows.Forms.Label()
        Me.lblReplayVersion = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnChangeTargetMap = New System.Windows.Forms.Button()
        Me.btnImportReplayVersion = New System.Windows.Forms.Button()
        Me.btnEditSelectedEntry = New System.Windows.Forms.Button()
        Me.mapOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.filterReplayControl = New ReplayExplorer.FilterControl()
        Me.replaySaveFileDialog = New System.Windows.Forms.SaveFileDialog()
        CType(Me.dataReplay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuMain.SuspendLayout()
        Me.panelEditControls.SuspendLayout()
        Me.SuspendLayout()
        '
        'dataReplay
        '
        Me.dataReplay.AllowUserToAddRows = False
        Me.dataReplay.AllowUserToDeleteRows = False
        Me.dataReplay.AllowUserToResizeColumns = False
        Me.dataReplay.AllowUserToResizeRows = False
        Me.dataReplay.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dataReplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataReplay.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colEntryType, Me.colGameTime, Me.colDescription})
        Me.dataReplay.Location = New System.Drawing.Point(0, 27)
        Me.dataReplay.Name = "dataReplay"
        Me.dataReplay.ReadOnly = True
        Me.dataReplay.RowHeadersVisible = False
        Me.dataReplay.Size = New System.Drawing.Size(671, 224)
        Me.dataReplay.TabIndex = 0
        '
        'colEntryType
        '
        Me.colEntryType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colEntryType.HeaderText = "Entry Type"
        Me.colEntryType.Name = "colEntryType"
        Me.colEntryType.ReadOnly = True
        Me.colEntryType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colEntryType.Width = 64
        '
        'colGameTime
        '
        Me.colGameTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colGameTime.HeaderText = "Time (ms)"
        Me.colGameTime.Name = "colGameTime"
        Me.colGameTime.ReadOnly = True
        Me.colGameTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colGameTime.Width = 58
        '
        'colDescription
        '
        Me.colDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colDescription.HeaderText = "Description"
        Me.colDescription.Name = "colDescription"
        Me.colDescription.ReadOnly = True
        Me.colDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'mnuMain
        '
        Me.mnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.EditToolStripMenuItem})
        Me.mnuMain.Location = New System.Drawing.Point(0, 0)
        Me.mnuMain.Name = "mnuMain"
        Me.mnuMain.Size = New System.Drawing.Size(671, 24)
        Me.mnuMain.TabIndex = 10
        Me.mnuMain.Text = "MenuStrip1"
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuOpen, Me.mnuSaveAs})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(37, 20)
        Me.mnuFile.Text = "File"
        '
        'mnuOpen
        '
        Me.mnuOpen.Name = "mnuOpen"
        Me.mnuOpen.Size = New System.Drawing.Size(152, 22)
        Me.mnuOpen.Text = "Open"
        '
        'mnuSaveAs
        '
        Me.mnuSaveAs.Enabled = False
        Me.mnuSaveAs.Name = "mnuSaveAs"
        Me.mnuSaveAs.Size = New System.Drawing.Size(152, 22)
        Me.mnuSaveAs.Text = "Save As ..."
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuModeExploreReplay, Me.mnuModeEditReplay})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(50, 20)
        Me.EditToolStripMenuItem.Text = "Mode"
        '
        'mnuModeExploreReplay
        '
        Me.mnuModeExploreReplay.Checked = True
        Me.mnuModeExploreReplay.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuModeExploreReplay.Name = "mnuModeExploreReplay"
        Me.mnuModeExploreReplay.Size = New System.Drawing.Size(150, 22)
        Me.mnuModeExploreReplay.Text = "Explore Replay"
        '
        'mnuModeEditReplay
        '
        Me.mnuModeEditReplay.Name = "mnuModeEditReplay"
        Me.mnuModeEditReplay.Size = New System.Drawing.Size(150, 22)
        Me.mnuModeEditReplay.Text = "Edit Replay"
        '
        'replayOpenFileDialog
        '
        Me.replayOpenFileDialog.Filter = "Warcraft 3 Replay Files (*.w3g)|*.w3g"
        '
        'txtDesc
        '
        Me.txtDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDesc.Location = New System.Drawing.Point(426, 257)
        Me.txtDesc.Multiline = True
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDesc.Size = New System.Drawing.Size(245, 199)
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
        Me.pbrLoadingReplay.Location = New System.Drawing.Point(369, 4)
        Me.pbrLoadingReplay.Name = "pbrLoadingReplay"
        Me.pbrLoadingReplay.Size = New System.Drawing.Size(290, 20)
        Me.pbrLoadingReplay.TabIndex = 21
        Me.pbrLoadingReplay.Visible = False
        '
        'lblProgress
        '
        Me.lblProgress.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblProgress.AutoSize = True
        Me.lblProgress.Location = New System.Drawing.Point(273, 9)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(90, 13)
        Me.lblProgress.TabIndex = 22
        Me.lblProgress.Text = "Loading Replay..."
        Me.lblProgress.Visible = False
        '
        'panelEditControls
        '
        Me.panelEditControls.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.panelEditControls.Controls.Add(Me.lblTargetMap)
        Me.panelEditControls.Controls.Add(Me.lblReplayVersion)
        Me.panelEditControls.Controls.Add(Me.Label3)
        Me.panelEditControls.Controls.Add(Me.Label2)
        Me.panelEditControls.Controls.Add(Me.Label1)
        Me.panelEditControls.Controls.Add(Me.btnChangeTargetMap)
        Me.panelEditControls.Controls.Add(Me.btnImportReplayVersion)
        Me.panelEditControls.Controls.Add(Me.btnEditSelectedEntry)
        Me.panelEditControls.Location = New System.Drawing.Point(0, 257)
        Me.panelEditControls.Name = "panelEditControls"
        Me.panelEditControls.Size = New System.Drawing.Size(420, 199)
        Me.panelEditControls.TabIndex = 24
        Me.panelEditControls.Visible = False
        '
        'lblTargetMap
        '
        Me.lblTargetMap.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTargetMap.Location = New System.Drawing.Point(158, 65)
        Me.lblTargetMap.Name = "lblTargetMap"
        Me.lblTargetMap.Size = New System.Drawing.Size(259, 30)
        Me.lblTargetMap.TabIndex = 7
        Me.lblTargetMap.Text = "Target Map"
        Me.lblTargetMap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblReplayVersion
        '
        Me.lblReplayVersion.AutoSize = True
        Me.lblReplayVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReplayVersion.Location = New System.Drawing.Point(158, 12)
        Me.lblReplayVersion.Name = "lblReplayVersion"
        Me.lblReplayVersion.Size = New System.Drawing.Size(92, 13)
        Me.lblReplayVersion.TabIndex = 6
        Me.lblReplayVersion.Text = "Replay Version"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(364, 26)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Allows you to update the replay's version, by importing it from another replay." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & _
            "(Can make old replays compatible, but the game may play out differently.)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 98)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(282, 26)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Allows you to select the map the replay says it applies to." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(The new map should " & _
            "be extremely similar to the true map.)"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 160)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(290, 26)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Allows you to edit the raw data making up a replay entry." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Intended for advanced" & _
            " users who understand the protocol.)"
        '
        'btnChangeTargetMap
        '
        Me.btnChangeTargetMap.Enabled = False
        Me.btnChangeTargetMap.Location = New System.Drawing.Point(12, 65)
        Me.btnChangeTargetMap.Name = "btnChangeTargetMap"
        Me.btnChangeTargetMap.Size = New System.Drawing.Size(140, 30)
        Me.btnChangeTargetMap.TabIndex = 2
        Me.btnChangeTargetMap.Text = "Change Target Map"
        Me.btnChangeTargetMap.UseVisualStyleBackColor = True
        '
        'btnImportReplayVersion
        '
        Me.btnImportReplayVersion.Enabled = False
        Me.btnImportReplayVersion.Location = New System.Drawing.Point(12, 3)
        Me.btnImportReplayVersion.Name = "btnImportReplayVersion"
        Me.btnImportReplayVersion.Size = New System.Drawing.Size(140, 30)
        Me.btnImportReplayVersion.TabIndex = 1
        Me.btnImportReplayVersion.Text = "Import Replay Version"
        Me.btnImportReplayVersion.UseVisualStyleBackColor = True
        '
        'btnEditSelectedEntry
        '
        Me.btnEditSelectedEntry.Enabled = False
        Me.btnEditSelectedEntry.Location = New System.Drawing.Point(12, 127)
        Me.btnEditSelectedEntry.Name = "btnEditSelectedEntry"
        Me.btnEditSelectedEntry.Size = New System.Drawing.Size(140, 30)
        Me.btnEditSelectedEntry.TabIndex = 0
        Me.btnEditSelectedEntry.Text = "Edit Selected Entry"
        Me.btnEditSelectedEntry.UseVisualStyleBackColor = True
        '
        'mapOpenFileDialog
        '
        Me.mapOpenFileDialog.Filter = "WC3 Map Files (*.w3m;*.w3x)|*.w3m;*.w3x"
        '
        'filterReplayControl
        '
        Me.filterReplayControl.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.filterReplayControl.AutoScroll = True
        Me.filterReplayControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.filterReplayControl.Location = New System.Drawing.Point(0, 257)
        Me.filterReplayControl.Name = "filterReplayControl"
        Me.filterReplayControl.Size = New System.Drawing.Size(420, 199)
        Me.filterReplayControl.TabIndex = 23
        '
        'replaySaveFileDialog
        '
        Me.replaySaveFileDialog.Filter = "Warcraft 3 Replays (*.w3g)|*.w3g"
        '
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(671, 457)
        Me.Controls.Add(Me.panelEditControls)
        Me.Controls.Add(Me.filterReplayControl)
        Me.Controls.Add(Me.pbrLoadingReplay)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.dataReplay)
        Me.Controls.Add(Me.mnuMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.mnuMain
        Me.Name = "FrmMain"
        Me.Text = "{ProductName}"
        CType(Me.dataReplay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuMain.ResumeLayout(False)
        Me.mnuMain.PerformLayout()
        Me.panelEditControls.ResumeLayout(False)
        Me.panelEditControls.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dataReplay As System.Windows.Forms.DataGridView
    Friend WithEvents mnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuOpen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents replayOpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents lscActionTypeFilter As System.Windows.Forms.CheckedListBox
    Friend WithEvents chkSkipEmptyTicks As System.Windows.Forms.CheckBox
    Friend WithEvents pbrLoadingReplay As System.Windows.Forms.ProgressBar
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents colEntryType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colGameTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSaveAs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents filterReplayControl As ReplayExplorer.FilterControl
    Friend WithEvents panelEditControls As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnChangeTargetMap As System.Windows.Forms.Button
    Friend WithEvents btnImportReplayVersion As System.Windows.Forms.Button
    Friend WithEvents btnEditSelectedEntry As System.Windows.Forms.Button
    Friend WithEvents mnuModeExploreReplay As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuModeEditReplay As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mapOpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents replaySaveFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents lblTargetMap As System.Windows.Forms.Label
    Friend WithEvents lblReplayVersion As System.Windows.Forms.Label

End Class
