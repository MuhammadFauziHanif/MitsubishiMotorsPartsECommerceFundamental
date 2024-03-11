Imports MitsubishiMotorsPartsECommerce.BLL

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Try
            Dim _adminBLL As New AdminBLL()
            Dim _adminDto = _adminBLL.Login(txtUsername.Text, txtPassword.Text)
            If _adminDto IsNot Nothing Then
                Session("Admin") = _adminDto

                Dim returnUrl = Request.QueryString("ReturnUrl")
                If Not String.IsNullOrEmpty(returnUrl) Then
                    Response.Redirect("~/" & returnUrl)
                Else
                    Response.Redirect("~/Default.aspx")
                End If
            Else
                ltMessage.Text = "<br/><span class='alert alert-danger'>Error: Invalid Email / Password </span><br/><br/>"
            End If
        Catch ex As Exception
            ltMessage.Text = "<br/><span class='alert alert-danger'>Error: " & ex.Message & "</span><br/><br/>"
        End Try
    End Sub
End Class