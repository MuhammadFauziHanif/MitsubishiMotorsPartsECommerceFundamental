Imports MitsubishiMotorsPartsECommerce.BLL.DTOs

Public Class SiteMaster
    Inherits MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim _adminDto = CType(Session("Admin"), AdminDTO)
            If _adminDto Is Nothing Then
                Response.Redirect("~/LoginPage.aspx")
            End If
            If _adminDto IsNot Nothing Then
                If _adminDto.Role = "superadmin" Then
                    admin.Visible = True
                End If
            End If
        End If
    End Sub
End Class