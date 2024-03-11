Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports Dapper
Imports Dapper.SqlMapper
Imports MitsubishiMotorsPartsECommerce.BO

Public Class AdminDAL
    Implements IAdminDAL

    Private Function GetConnectionString() As String
        Return ConfigurationManager.ConnectionStrings("MyDbConnectionString").ConnectionString
    End Function

    Public Sub ChangePassword(username As String, newPassword As String) Implements IAdminDAL.ChangePassword
        Throw New NotImplementedException()
    End Sub

    Public Sub Insert(entity As Admin) Implements ICrudDAL(Of Admin).Insert
        Using conn As New SqlConnection(GetConnectionString())
            Dim strSql As String = "INSERT INTO Admin(Username, Email, Password, Role) VALUES(@Username, @Email, @Password, @Role)"
            Dim param = New With {
                Key .Username = entity.Username,
                Key .Email = entity.Email,
                Key .Password = entity.Password,
                Key .Role = entity.Role
            }
            Try
                Dim result = conn.Execute(strSql, param)
                If result <> 1 Then
                    Throw New ArgumentException("Admin not created")
                End If
            Catch sqlEx As SqlException
                Throw New ArgumentException($"{sqlEx.InnerException.Message} - {sqlEx.Number}")
            Catch ex As Exception
                Throw New ArgumentException(ex.Message)
            End Try
        End Using
    End Sub

    Public Sub Update(entity As Admin) Implements ICrudDAL(Of Admin).Update
        Throw New NotImplementedException()
    End Sub

    Public Sub Delete(id As Integer) Implements ICrudDAL(Of Admin).Delete
        Throw New NotImplementedException()
    End Sub

    Public Function GetByUsername(username As String) As Admin Implements IAdminDAL.GetByUsername
        Throw New NotImplementedException()
    End Function

    Public Function Login(username As String, password As String) As Admin Implements IAdminDAL.Login
        Using conn As New SqlConnection(GetConnectionString())
            Dim strSql As String = "SELECT * FROM Admin WHERE Username = @Username and Password = @Password"
            Dim param = New With {
                            Key .Username = username,
                            Key .Password = password
                        }
            Dim result = conn.QueryFirstOrDefault(Of Admin)(strSql, param)
            If result Is Nothing Then
                Throw New ArgumentException("Invalid username or password")
            End If

            Return result
        End Using
    End Function

    Public Function GetAll() As IEnumerable(Of Admin) Implements ICrudDAL(Of Admin).GetAll
        Using conn As New SqlConnection(GetConnectionString())
            Dim strSql As String = "SELECT * FROM Admin"
            Dim results = conn.Query(Of Admin)(strSql)
            Return results
        End Using
    End Function

    Public Function GetById(id As Integer) As Admin Implements ICrudDAL(Of Admin).GetById
        Throw New NotImplementedException()
    End Function
End Class
