Public Class FormPrueba
	Private Sub ButtonSet_Click(sender As Object, e As EventArgs) Handles ButtonSet.Click
		Vumetro1.SetValor(CInt(TextBox1.Text))
	End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        Vumetro1.SetValor(TrackBar1.Value)
    End Sub
End Class
