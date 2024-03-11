Imports MitsubishiMotorsPartsECommerce.BO

Public Interface IAdminDAL
    Inherits ICrudDAL(Of Admin)
    Function GetByUsername(username As String) As Admin
    Function Login(ByVal username As String, ByVal password As String) As Admin
    Sub ChangePassword(ByVal username As String, ByVal newPassword As String)
End Interface
