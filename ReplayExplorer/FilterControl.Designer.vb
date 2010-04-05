<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FilterControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.lscEntryTypeFilter = New System.Windows.Forms.CheckedListBox()
        Me.btnNoActionTypes = New System.Windows.Forms.Button()
        Me.btnAllActionTypes = New System.Windows.Forms.Button()
        Me.lscActionTypes = New System.Windows.Forms.CheckedListBox()
        Me.chkIgnoreEmptyTicks = New System.Windows.Forms.CheckBox()
        Me.lblMaxGameTime = New System.Windows.Forms.Label()
        Me.lblMinGameTime = New System.Windows.Forms.Label()
        Me.txtMaxGameTime = New System.Windows.Forms.TextBox()
        Me.txtMinGameTime = New System.Windows.Forms.TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.chkIgnoreGameStateChecksums = New System.Windows.Forms.CheckBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.lscFilterPlayers = New System.Windows.Forms.CheckedListBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.lblReplayVersion = New System.Windows.Forms.Label()
        Me.lblTargetMap = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.SuspendLayout()
        '
        'lscEntryTypeFilter
        '
        Me.lscEntryTypeFilter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lscEntryTypeFilter.FormattingEnabled = True
        Me.lscEntryTypeFilter.HorizontalScrollbar = True
        Me.lscEntryTypeFilter.Location = New System.Drawing.Point(3, 3)
        Me.lscEntryTypeFilter.Name = "lscEntryTypeFilter"
        Me.lscEntryTypeFilter.Size = New System.Drawing.Size(526, 139)
        Me.lscEntryTypeFilter.TabIndex = 33
        '
        'btnNoActionTypes
        '
        Me.btnNoActionTypes.Location = New System.Drawing.Point(6, 34)
        Me.btnNoActionTypes.Name = "btnNoActionTypes"
        Me.btnNoActionTypes.Size = New System.Drawing.Size(92, 25)
        Me.btnNoActionTypes.TabIndex = 31
        Me.btnNoActionTypes.Text = "Select None"
        Me.btnNoActionTypes.UseVisualStyleBackColor = True
        '
        'btnAllActionTypes
        '
        Me.btnAllActionTypes.Location = New System.Drawing.Point(6, 3)
        Me.btnAllActionTypes.Name = "btnAllActionTypes"
        Me.btnAllActionTypes.Size = New System.Drawing.Size(92, 25)
        Me.btnAllActionTypes.TabIndex = 30
        Me.btnAllActionTypes.Text = "Select All"
        Me.btnAllActionTypes.UseVisualStyleBackColor = True
        '
        'lscActionTypes
        '
        Me.lscActionTypes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lscActionTypes.FormattingEnabled = True
        Me.lscActionTypes.HorizontalScrollbar = True
        Me.lscActionTypes.IntegralHeight = False
        Me.lscActionTypes.Location = New System.Drawing.Point(104, 0)
        Me.lscActionTypes.Name = "lscActionTypes"
        Me.lscActionTypes.Size = New System.Drawing.Size(428, 145)
        Me.lscActionTypes.TabIndex = 29
        '
        'chkIgnoreEmptyTicks
        '
        Me.chkIgnoreEmptyTicks.AutoSize = True
        Me.chkIgnoreEmptyTicks.Checked = True
        Me.chkIgnoreEmptyTicks.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIgnoreEmptyTicks.Location = New System.Drawing.Point(0, 80)
        Me.chkIgnoreEmptyTicks.Name = "chkIgnoreEmptyTicks"
        Me.chkIgnoreEmptyTicks.Size = New System.Drawing.Size(117, 17)
        Me.chkIgnoreEmptyTicks.TabIndex = 27
        Me.chkIgnoreEmptyTicks.Text = "Ignore Empty Ticks"
        Me.chkIgnoreEmptyTicks.UseVisualStyleBackColor = True
        '
        'lblMaxGameTime
        '
        Me.lblMaxGameTime.AutoSize = True
        Me.lblMaxGameTime.Location = New System.Drawing.Point(115, 38)
        Me.lblMaxGameTime.Name = "lblMaxGameTime"
        Me.lblMaxGameTime.Size = New System.Drawing.Size(84, 13)
        Me.lblMaxGameTime.TabIndex = 24
        Me.lblMaxGameTime.Text = "Max Game Time"
        '
        'lblMinGameTime
        '
        Me.lblMinGameTime.AutoSize = True
        Me.lblMinGameTime.Location = New System.Drawing.Point(-3, 38)
        Me.lblMinGameTime.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.lblMinGameTime.Name = "lblMinGameTime"
        Me.lblMinGameTime.Size = New System.Drawing.Size(81, 13)
        Me.lblMinGameTime.TabIndex = 23
        Me.lblMinGameTime.Text = "Min Game Time"
        '
        'txtMaxGameTime
        '
        Me.txtMaxGameTime.Location = New System.Drawing.Point(118, 54)
        Me.txtMaxGameTime.Name = "txtMaxGameTime"
        Me.txtMaxGameTime.Size = New System.Drawing.Size(112, 20)
        Me.txtMaxGameTime.TabIndex = 22
        '
        'txtMinGameTime
        '
        Me.txtMinGameTime.Location = New System.Drawing.Point(0, 54)
        Me.txtMinGameTime.Name = "txtMinGameTime"
        Me.txtMinGameTime.Size = New System.Drawing.Size(112, 20)
        Me.txtMinGameTime.TabIndex = 21
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(540, 171)
        Me.TabControl1.TabIndex = 34
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.lblTargetMap)
        Me.TabPage1.Controls.Add(Me.lblReplayVersion)
        Me.TabPage1.Controls.Add(Me.chkIgnoreGameStateChecksums)
        Me.TabPage1.Controls.Add(Me.lblMinGameTime)
        Me.TabPage1.Controls.Add(Me.txtMinGameTime)
        Me.TabPage1.Controls.Add(Me.txtMaxGameTime)
        Me.TabPage1.Controls.Add(Me.lblMaxGameTime)
        Me.TabPage1.Controls.Add(Me.chkIgnoreEmptyTicks)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(532, 145)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'chkIgnoreGameStateChecksums
        '
        Me.chkIgnoreGameStateChecksums.AutoSize = True
        Me.chkIgnoreGameStateChecksums.Checked = True
        Me.chkIgnoreGameStateChecksums.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIgnoreGameStateChecksums.Location = New System.Drawing.Point(0, 103)
        Me.chkIgnoreGameStateChecksums.Name = "chkIgnoreGameStateChecksums"
        Me.chkIgnoreGameStateChecksums.Size = New System.Drawing.Size(173, 17)
        Me.chkIgnoreGameStateChecksums.TabIndex = 28
        Me.chkIgnoreGameStateChecksums.Text = "Ignore Game State Checksums"
        Me.chkIgnoreGameStateChecksums.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.lscFilterPlayers)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(532, 145)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Players"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'lscFilterPlayers
        '
        Me.lscFilterPlayers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lscFilterPlayers.FormattingEnabled = True
        Me.lscFilterPlayers.Location = New System.Drawing.Point(3, 3)
        Me.lscFilterPlayers.Name = "lscFilterPlayers"
        Me.lscFilterPlayers.Size = New System.Drawing.Size(526, 139)
        Me.lscFilterPlayers.TabIndex = 27
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.lscEntryTypeFilter)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(532, 145)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Entry Types"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.lscActionTypes)
        Me.TabPage4.Controls.Add(Me.btnAllActionTypes)
        Me.TabPage4.Controls.Add(Me.btnNoActionTypes)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(532, 145)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Action Types"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'lblReplayVersion
        '
        Me.lblReplayVersion.AutoSize = True
        Me.lblReplayVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReplayVersion.Location = New System.Drawing.Point(-3, 0)
        Me.lblReplayVersion.Margin = New System.Windows.Forms.Padding(3)
        Me.lblReplayVersion.Name = "lblReplayVersion"
        Me.lblReplayVersion.Size = New System.Drawing.Size(104, 13)
        Me.lblReplayVersion.TabIndex = 29
        Me.lblReplayVersion.Text = "Replay Version: -"
        '
        'lblTargetMap
        '
        Me.lblTargetMap.AutoSize = True
        Me.lblTargetMap.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTargetMap.Location = New System.Drawing.Point(-3, 19)
        Me.lblTargetMap.Margin = New System.Windows.Forms.Padding(3)
        Me.lblTargetMap.Name = "lblTargetMap"
        Me.lblTargetMap.Size = New System.Drawing.Size(84, 13)
        Me.lblTargetMap.TabIndex = 30
        Me.lblTargetMap.Text = "Target Map: -"
        '
        'FilterControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "FilterControl"
        Me.Size = New System.Drawing.Size(540, 171)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lscEntryTypeFilter As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnNoActionTypes As System.Windows.Forms.Button
    Friend WithEvents btnAllActionTypes As System.Windows.Forms.Button
    Friend WithEvents lscActionTypes As System.Windows.Forms.CheckedListBox
    Friend WithEvents chkIgnoreEmptyTicks As System.Windows.Forms.CheckBox
    Friend WithEvents lblMaxGameTime As System.Windows.Forms.Label
    Friend WithEvents lblMinGameTime As System.Windows.Forms.Label
    Friend WithEvents txtMaxGameTime As System.Windows.Forms.TextBox
    Friend WithEvents txtMinGameTime As System.Windows.Forms.TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents lscFilterPlayers As System.Windows.Forms.CheckedListBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents chkIgnoreGameStateChecksums As System.Windows.Forms.CheckBox
    Friend WithEvents lblTargetMap As System.Windows.Forms.Label
    Friend WithEvents lblReplayVersion As System.Windows.Forms.Label

End Class
