<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AsyncReplayDataControl
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
        Me.dataReplay = New System.Windows.Forms.DataGridView()
        Me.colGameTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colEntry = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dataReplay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dataReplay
        '
        Me.dataReplay.AllowUserToAddRows = False
        Me.dataReplay.AllowUserToDeleteRows = False
        Me.dataReplay.AllowUserToResizeColumns = False
        Me.dataReplay.AllowUserToResizeRows = False
        Me.dataReplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataReplay.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colGameTime, Me.colEntry})
        Me.dataReplay.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dataReplay.Location = New System.Drawing.Point(0, 0)
        Me.dataReplay.Name = "dataReplay"
        Me.dataReplay.ReadOnly = True
        Me.dataReplay.RowHeadersVisible = False
        Me.dataReplay.Size = New System.Drawing.Size(944, 474)
        Me.dataReplay.TabIndex = 1
        '
        'colGameTime
        '
        Me.colGameTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.colGameTime.HeaderText = "Time (ms) "
        Me.colGameTime.Name = "colGameTime"
        Me.colGameTime.ReadOnly = True
        Me.colGameTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colGameTime.Width = 61
        '
        'colEntry
        '
        Me.colEntry.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.colEntry.HeaderText = "Entry"
        Me.colEntry.Name = "colEntry"
        Me.colEntry.ReadOnly = True
        Me.colEntry.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'AsyncReplayDataControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.dataReplay)
        Me.Name = "AsyncReplayDataControl"
        Me.Size = New System.Drawing.Size(944, 474)
        CType(Me.dataReplay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dataReplay As System.Windows.Forms.DataGridView
    Friend WithEvents colGameTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEntry As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
