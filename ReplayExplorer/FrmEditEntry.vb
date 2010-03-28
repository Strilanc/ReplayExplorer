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
            txtRawData.Text = pickle.Data.ToHexString
            txtParsed.Text = pickle.Description.Value
        Finally
            allowEvents = True
        End Try
    End Sub
    Private Function TrySavePickle() As Boolean
        Try
            allowEvents = False
            Dim data = (From word In txtRawData.Text.Replace(Environment.NewLine, " "c).Split(" "c)
                        Where word <> ""
                        Select CByte(word.FromHexToUInt64(ByteOrder.BigEndian))
                        ).ToReadableList
            Dim pickle = jar.Parse(data)
            Me.pickle = pickle
            Me.txtParsed.Text = pickle.Description.Value
            If pickle.Data.Count < data.Count Then
                txtParsed.Text = "Warning: Data leftover [{0}]".Frmt(data.SubView(pickle.Data.Count).ToHexString) + Environment.NewLine + txtParsed.Text
            End If
            Return True
        Catch ex As Exception
            Me.txtParsed.Text = ex.ToString
            Return False
        Finally
            allowEvents = True
        End Try
    End Function

    Private Sub txtRawData_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRawData.TextChanged
        If Not allowEvents Then Return
        btnApply.Enabled = TrySavePickle()
    End Sub

    Private Sub btnApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Me.Dispose()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        pickle = Nothing
        Me.Dispose()
    End Sub
End Class
