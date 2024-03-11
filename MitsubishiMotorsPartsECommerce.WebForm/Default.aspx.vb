Imports MitsubishiMotorsPartsECommerce.BLL.DTOs

Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("Admin") IsNot Nothing Then
                Dim _adminDto As AdminDTO = CType(Session("Admin"), AdminDTO)
                ' Check if it is superadmin if yes then show registerAdminSection
                If _adminDto.Role = "superadmin" Then
                    registerAdminSection.Visible = True
                End If
            End If
        End If
    End Sub
End Class