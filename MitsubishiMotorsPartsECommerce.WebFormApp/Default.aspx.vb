Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub BtnHello_Click(sender As Object, e As EventArgs) Handles BtnHello.Click
        LblHello.Text = "Hello! 😊 "

    End Sub
End Class