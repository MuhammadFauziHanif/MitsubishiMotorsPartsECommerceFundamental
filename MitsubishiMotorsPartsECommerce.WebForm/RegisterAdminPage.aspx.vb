Imports MitsubishiMotorsPartsECommerce.BLL
Imports MitsubishiMotorsPartsECommerce.BLL.DTOs

Public Class RegisterAdminPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim _adminDto = CType(Session("Admin"), AdminDTO)
            If _adminDto Is Nothing Then
                Response.Redirect("~/LoginPage.aspx")
            End If

            If _adminDto IsNot Nothing Then
                If _adminDto.Role <> "superadmin" Then
                    Response.Redirect("~/LoginPage.aspx")
                End If
            End If
        End If
    End Sub

    Protected Sub btnRegistration_Click(sender As Object, e As EventArgs)
        Try
            Dim _adminBLL As New AdminBLL()
            Dim _adminDto As New AdminCreateDTO()
            _adminDto.Username = txtUsername.Text
            _adminDto.Password = txtPassword.Text
            _adminDto.Email = txtEmail.Text
            _adminDto.Role = ddlRole.Text

            _adminBLL.Insert(_adminDto)

            ltMessage.Text = "<span class='alert alert-success'>User Registration Success</span><br/><br/>"
        Catch ex As Exception
            ltMessage.Text = "<span class='alert alert-danger'>Error: " & ex.Message & "</span><br/><br/>"
        End Try
    End Sub

End Class