Imports Tinker
Imports Tinker.Pickling
Imports Strilbrary.Time
Imports Strilbrary.Values
Imports Strilbrary.Threading
Imports Strilbrary.Collections
Imports Tinker.WC3.Replay
Imports Tinker.WC3

Public Class FrmEditEntry
    Private jar As ISimpleJar
    Private pickle As ISimplePickle
    Private allowEvents As Boolean
    Private control As ISimpleValueEditor
    Private throttle As New Throttle(cooldown:=100.Milliseconds, clock:=New SystemClock())

    Public Shared Function EditEntry(ByVal owner As IWin32Window, ByVal jar As ISimpleJar, ByVal pickle As ISimplePickle) As ISimplePickle
        Using f = New FrmEditEntry
            f.LoadPickle(jar, pickle)
            f.ShowDialog(owner)
            Return f.pickle
        End Using
    End Function
    Public Sub LoadPickle(ByVal jar As ISimpleJar, ByVal pickle As ISimplePickle)
        Try
            allowEvents = False
            Me.jar = jar
            Me.pickle = pickle
            UpdateRawData(pickle.Data)
            txtParsed.Text = pickle.Description

            Me.control = jar.MakeControl()
            control.Control.Width = Panel1.Width
            control.Control.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top
            Panel1.Controls.Add(control.Control)
            AddHandler control.ValueChanged, Sub() StructuredUpdate()

            RefreshStructuredView()
        Finally
            allowEvents = True
        End Try
    End Sub
    Private Sub RecursiveAddValidatedHandler(ByVal control As Control, ByVal action As EventHandler)
        AddHandler control.Validated, action
        For Each child As Control In control.Controls
            RecursiveAddValidatedHandler(child, action)
        Next child
    End Sub
    Private Sub RefreshStructuredView()
        Try
            allowEvents = False
            control.Control.Enabled = True
            control.Value = pickle.Value
        Finally
            allowEvents = True
        End Try
    End Sub

    Private Sub StructuredUpdate()
        If Not allowEvents Then Return
        Try
            allowEvents = False
            Dim pickle = jar.PackPickle(control.Value)
            Me.pickle = pickle
            Me.txtParsed.Text = pickle.Description
            UpdateRawData(pickle.Data)
            txtRawData.Enabled = True
            txtParsed.BackColor = SystemColors.Window
            btnApply.Enabled = True
        Catch ex As Exception
            Me.txtParsed.Text = ex.ToString
            txtParsed.BackColor = Color.Pink
            txtRawData.Enabled = False
            btnApply.Enabled = False
        Finally
            allowEvents = True
        End Try
    End Sub

    Private Function Linefy(ByVal tree As Tree(Of JarSegment)) As String
        If tree.Children.None Then
            Return tree.Value.Data.ToHexString
        Else
            Dim lines = New List(Of String)
            For Each child In tree.Children
                lines.Add(Linefy(child))
            Next child
            If tree.Children.Count = 1 Then
                Return lines.Single
            ElseIf (From child In tree.Children Where child.Value.Data.Count <> 1).None Then
                Return lines.StringJoin(" ")
            ElseIf tree.Children.Count = 2 Then
                Return lines.First + Environment.NewLine + lines.Skip(1).StringJoin(Environment.NewLine).Indent("  ")
            Else
                Return lines.StringJoin(Environment.NewLine).Indent("  ")
            End If
        End If
    End Function
    Private Sub UpdateRawData(ByVal data As IReadableList(Of Byte))
        txtRawData.Text = Linefy(jar.RecursiveSegment(data))
    End Sub

    Private Sub OnRawDataChanged() Handles txtRawData.TextChanged
        If Not allowEvents Then Return

        btnApply.Enabled = True
        txtParsed.BackColor = SystemColors.Window
        txtParsed.ReadOnly = False

        Try
            allowEvents = False
            Dim data = New DataJar().Parse(txtRawData.Text.Replace(Environment.NewLine, " "c))
            Me.pickle = jar.ParsePickle(data)
            txtParsed.Text = pickle.Description
            If pickle.Data.Count < data.Count Then
                txtParsed.Text = "Warning: Data leftover [{0}]".Frmt(data.SubView(pickle.Data.Count).ToHexString) + Environment.NewLine +
                                 txtParsed.Text
                txtParsed.BackColor = Color.LightYellow
            End If
            control.Control.Enabled = False
            throttle.SetActionToRun(Sub() Me.Invoke(Sub() RefreshStructuredView()))
        Catch ex As Exception
            txtParsed.Text = ex.ToString
            txtParsed.BackColor = Color.Pink
            txtParsed.ReadOnly = True
            btnApply.Enabled = False
        Finally
            allowEvents = True
        End Try
    End Sub
    Private Sub OnParsedTextChanged() Handles txtParsed.TextChanged
        If Not allowEvents Then Return

        btnApply.Enabled = True
        txtRawData.ReadOnly = False
        txtRawData.BackColor = SystemColors.Window

        Try
            allowEvents = False
            Me.pickle = jar.PackPickle(jar.Parse(txtParsed.Text))
            UpdateRawData(pickle.Data)
            control.Control.Enabled = False
            throttle.SetActionToRun(Sub() Me.Invoke(Sub() RefreshStructuredView()))
        Catch ex As Exception
            txtRawData.Text = ex.ToString
            txtRawData.BackColor = Color.Pink
            btnApply.Enabled = False
            txtRawData.ReadOnly = True
        Finally
            allowEvents = True
        End Try
    End Sub

    Private Sub btnApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Me.Dispose()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        pickle = Nothing
        Me.Dispose()
    End Sub
End Class
