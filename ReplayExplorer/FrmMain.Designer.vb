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
        Me.txtMinGameTime = New System.Windows.Forms.TextBox()
        Me.txtMaxGameTime = New System.Windows.Forms.TextBox()
        Me.lblMinGameTime = New System.Windows.Forms.Label()
        Me.lblMaxGameTime = New System.Windows.Forms.Label()
        Me.lblPlayerFilter = New System.Windows.Forms.Label()
        Me.lscFilterPlayers = New System.Windows.Forms.CheckedListBox()
        Me.mnuMain = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOpen = New System.Windows.Forms.ToolStripMenuItem()
        Me.replayOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.chkIgnoreEmptyTicks = New System.Windows.Forms.CheckBox()
        Me.lscActionTypes = New System.Windows.Forms.CheckedListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAllActionTypes = New System.Windows.Forms.Button()
        Me.btnNoActionTypes = New System.Windows.Forms.Button()
        Me.lscEntryTypeFilter = New System.Windows.Forms.CheckedListBox()
        Me.lblEntryTypeFilter = New System.Windows.Forms.Label()
        Me.lscActionTypeFilter = New System.Windows.Forms.CheckedListBox()
        Me.chkSkipEmptyTicks = New System.Windows.Forms.CheckBox()
        Me.pbrLoadingReplay = New System.Windows.Forms.ProgressBar()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.colEntryType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colGameTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dataReplay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuMain.SuspendLayout()
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
        Me.dataReplay.Size = New System.Drawing.Size(996, 250)
        Me.dataReplay.TabIndex = 0
        '
        'txtMinGameTime
        '
        Me.txtMinGameTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtMinGameTime.Location = New System.Drawing.Point(12, 296)
        Me.txtMinGameTime.Name = "txtMinGameTime"
        Me.txtMinGameTime.Size = New System.Drawing.Size(112, 20)
        Me.txtMinGameTime.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtMinGameTime, "Entries before this game time will be ignored." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Leave blank to not use a minimum." & _
                "")
        '
        'txtMaxGameTime
        '
        Me.txtMaxGameTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtMaxGameTime.Location = New System.Drawing.Point(12, 336)
        Me.txtMaxGameTime.Name = "txtMaxGameTime"
        Me.txtMaxGameTime.Size = New System.Drawing.Size(112, 20)
        Me.txtMaxGameTime.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.txtMaxGameTime, "Entries after this game time will be ignored." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Leave blank to not use a maximum.")
        '
        'lblMinGameTime
        '
        Me.lblMinGameTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblMinGameTime.AutoSize = True
        Me.lblMinGameTime.Location = New System.Drawing.Point(12, 280)
        Me.lblMinGameTime.Name = "lblMinGameTime"
        Me.lblMinGameTime.Size = New System.Drawing.Size(81, 13)
        Me.lblMinGameTime.TabIndex = 3
        Me.lblMinGameTime.Text = "Min Game Time"
        '
        'lblMaxGameTime
        '
        Me.lblMaxGameTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblMaxGameTime.AutoSize = True
        Me.lblMaxGameTime.Location = New System.Drawing.Point(12, 320)
        Me.lblMaxGameTime.Name = "lblMaxGameTime"
        Me.lblMaxGameTime.Size = New System.Drawing.Size(84, 13)
        Me.lblMaxGameTime.TabIndex = 4
        Me.lblMaxGameTime.Text = "Max Game Time"
        '
        'lblPlayerFilter
        '
        Me.lblPlayerFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblPlayerFilter.AutoSize = True
        Me.lblPlayerFilter.Location = New System.Drawing.Point(265, 280)
        Me.lblPlayerFilter.Name = "lblPlayerFilter"
        Me.lblPlayerFilter.Size = New System.Drawing.Size(94, 13)
        Me.lblPlayerFilter.TabIndex = 5
        Me.lblPlayerFilter.Text = "Action Player Filter"
        '
        'lscFilterPlayers
        '
        Me.lscFilterPlayers.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lscFilterPlayers.FormattingEnabled = True
        Me.lscFilterPlayers.Location = New System.Drawing.Point(268, 296)
        Me.lscFilterPlayers.Name = "lscFilterPlayers"
        Me.lscFilterPlayers.Size = New System.Drawing.Size(140, 184)
        Me.lscFilterPlayers.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.lscFilterPlayers, "Ticks which don't have actions from any of these players will be skipped.")
        '
        'mnuMain
        '
        Me.mnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile})
        Me.mnuMain.Location = New System.Drawing.Point(0, 0)
        Me.mnuMain.Name = "mnuMain"
        Me.mnuMain.Size = New System.Drawing.Size(996, 24)
        Me.mnuMain.TabIndex = 10
        Me.mnuMain.Text = "MenuStrip1"
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuOpen})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(37, 20)
        Me.mnuFile.Text = "File"
        '
        'mnuOpen
        '
        Me.mnuOpen.Name = "mnuOpen"
        Me.mnuOpen.Size = New System.Drawing.Size(103, 22)
        Me.mnuOpen.Text = "Open"
        '
        'txtDesc
        '
        Me.txtDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDesc.Location = New System.Drawing.Point(560, 283)
        Me.txtDesc.Multiline = True
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtDesc.Size = New System.Drawing.Size(436, 199)
        Me.txtDesc.TabIndex = 11
        Me.txtDesc.WordWrap = False
        '
        'chkIgnoreEmptyTicks
        '
        Me.chkIgnoreEmptyTicks.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkIgnoreEmptyTicks.AutoSize = True
        Me.chkIgnoreEmptyTicks.Checked = True
        Me.chkIgnoreEmptyTicks.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIgnoreEmptyTicks.Location = New System.Drawing.Point(12, 362)
        Me.chkIgnoreEmptyTicks.Name = "chkIgnoreEmptyTicks"
        Me.chkIgnoreEmptyTicks.Size = New System.Drawing.Size(112, 17)
        Me.chkIgnoreEmptyTicks.TabIndex = 13
        Me.chkIgnoreEmptyTicks.Text = "Ignore empty ticks"
        Me.ToolTip1.SetToolTip(Me.chkIgnoreEmptyTicks, "Determines if ticks with no actions are shown.")
        Me.chkIgnoreEmptyTicks.UseVisualStyleBackColor = True
        '
        'lscActionTypes
        '
        Me.lscActionTypes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lscActionTypes.FormattingEnabled = True
        Me.lscActionTypes.HorizontalScrollbar = True
        Me.lscActionTypes.Location = New System.Drawing.Point(414, 296)
        Me.lscActionTypes.Name = "lscActionTypes"
        Me.lscActionTypes.Size = New System.Drawing.Size(140, 169)
        Me.lscActionTypes.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.lscActionTypes, "Ticks which don't have any of these action types will be skipped.")
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(411, 280)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Action Type Filter"
        '
        'btnAllActionTypes
        '
        Me.btnAllActionTypes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAllActionTypes.Location = New System.Drawing.Point(489, 464)
        Me.btnAllActionTypes.Name = "btnAllActionTypes"
        Me.btnAllActionTypes.Size = New System.Drawing.Size(65, 19)
        Me.btnAllActionTypes.TabIndex = 17
        Me.btnAllActionTypes.Text = "All"
        Me.btnAllActionTypes.UseVisualStyleBackColor = True
        '
        'btnNoActionTypes
        '
        Me.btnNoActionTypes.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNoActionTypes.Location = New System.Drawing.Point(414, 464)
        Me.btnNoActionTypes.Name = "btnNoActionTypes"
        Me.btnNoActionTypes.Size = New System.Drawing.Size(65, 19)
        Me.btnNoActionTypes.TabIndex = 18
        Me.btnNoActionTypes.Text = "None"
        Me.btnNoActionTypes.UseVisualStyleBackColor = True
        '
        'lscEntryTypeFilter
        '
        Me.lscEntryTypeFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lscEntryTypeFilter.FormattingEnabled = True
        Me.lscEntryTypeFilter.HorizontalScrollbar = True
        Me.lscEntryTypeFilter.Location = New System.Drawing.Point(133, 296)
        Me.lscEntryTypeFilter.Name = "lscEntryTypeFilter"
        Me.lscEntryTypeFilter.Size = New System.Drawing.Size(129, 184)
        Me.lscEntryTypeFilter.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.lscEntryTypeFilter, "Entries of a type not checked here will be ignored.")
        '
        'lblEntryTypeFilter
        '
        Me.lblEntryTypeFilter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblEntryTypeFilter.AutoSize = True
        Me.lblEntryTypeFilter.Location = New System.Drawing.Point(130, 280)
        Me.lblEntryTypeFilter.Name = "lblEntryTypeFilter"
        Me.lblEntryTypeFilter.Size = New System.Drawing.Size(83, 13)
        Me.lblEntryTypeFilter.TabIndex = 19
        Me.lblEntryTypeFilter.Text = "Entry Type Filter"
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
        Me.pbrLoadingReplay.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pbrLoadingReplay.Location = New System.Drawing.Point(12, 460)
        Me.pbrLoadingReplay.Name = "pbrLoadingReplay"
        Me.pbrLoadingReplay.Size = New System.Drawing.Size(115, 20)
        Me.pbrLoadingReplay.TabIndex = 21
        Me.pbrLoadingReplay.Visible = False
        '
        'lblProgress
        '
        Me.lblProgress.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblProgress.AutoSize = True
        Me.lblProgress.Location = New System.Drawing.Point(12, 444)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(90, 13)
        Me.lblProgress.TabIndex = 22
        Me.lblProgress.Text = "Loading Replay..."
        Me.lblProgress.Visible = False
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
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(996, 483)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.pbrLoadingReplay)
        Me.Controls.Add(Me.lscEntryTypeFilter)
        Me.Controls.Add(Me.lblEntryTypeFilter)
        Me.Controls.Add(Me.btnNoActionTypes)
        Me.Controls.Add(Me.btnAllActionTypes)
        Me.Controls.Add(Me.lscActionTypes)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.chkIgnoreEmptyTicks)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.lscFilterPlayers)
        Me.Controls.Add(Me.lblPlayerFilter)
        Me.Controls.Add(Me.lblMaxGameTime)
        Me.Controls.Add(Me.lblMinGameTime)
        Me.Controls.Add(Me.txtMaxGameTime)
        Me.Controls.Add(Me.txtMinGameTime)
        Me.Controls.Add(Me.dataReplay)
        Me.Controls.Add(Me.mnuMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.mnuMain
        Me.Name = "FrmMain"
        Me.Text = "{ProductName}"
        CType(Me.dataReplay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuMain.ResumeLayout(False)
        Me.mnuMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dataReplay As System.Windows.Forms.DataGridView
    Friend WithEvents txtMinGameTime As System.Windows.Forms.TextBox
    Friend WithEvents txtMaxGameTime As System.Windows.Forms.TextBox
    Friend WithEvents lblMinGameTime As System.Windows.Forms.Label
    Friend WithEvents lblMaxGameTime As System.Windows.Forms.Label
    Friend WithEvents lblPlayerFilter As System.Windows.Forms.Label
    Friend WithEvents lscFilterPlayers As System.Windows.Forms.CheckedListBox
    Friend WithEvents mnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuOpen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents replayOpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents chkIgnoreEmptyTicks As System.Windows.Forms.CheckBox
    Friend WithEvents lscActionTypes As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnAllActionTypes As System.Windows.Forms.Button
    Friend WithEvents btnNoActionTypes As System.Windows.Forms.Button
    Friend WithEvents lscEntryTypeFilter As System.Windows.Forms.CheckedListBox
    Friend WithEvents lblEntryTypeFilter As System.Windows.Forms.Label
    Friend WithEvents lscActionTypeFilter As System.Windows.Forms.CheckedListBox
    Friend WithEvents chkSkipEmptyTicks As System.Windows.Forms.CheckBox
    Friend WithEvents pbrLoadingReplay As System.Windows.Forms.ProgressBar
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents colEntryType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colGameTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

End Class
