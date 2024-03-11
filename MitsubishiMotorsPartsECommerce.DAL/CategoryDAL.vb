Imports Dapper
Imports MitsubishiMotorsPartsECommerce.BO
Imports MitsubishiMotorsPartsECommerce.DAL.Interfaces
Imports System.Configuration
Imports System.Data.SqlClient

Public Class CategoryDAL
    Implements ICategoryDAL

    Private Function GetConnectionString() As String
        'Return "Data Source=.\BSISQLEXPRESS;Initial Catalog=MitsubishiMotorsPartsECommerce;Integrated Security=True;TrustServerCertificate=True"
        Return ConfigurationManager.ConnectionStrings("MyDbConnectionString").ConnectionString
    End Function

    Public Sub Delete(ByVal id As Integer) Implements ICrudDAL(Of ProductCategory).Delete
        Using conn As New SqlConnection(GetConnectionString())
            Dim strSql As String = "DELETE FROM [ProductCategory] WHERE [CategoryID] = @CategoryID"
            Dim param = New With {Key .CategoryID = id}
            Try
                Dim result As Integer = conn.Execute(strSql, param)
                If result <> 1 Then
                    Throw New ArgumentException("Delete data failed..")
                End If
            Catch sqlEx As SqlException
                Throw New ArgumentException($"{sqlEx.InnerException.Message} - {sqlEx.Number}")
            Catch ex As Exception
                Throw New ArgumentException(ex.Message)
            End Try
        End Using
    End Sub

    Public Function GetAll() As IEnumerable(Of ProductCategory) Implements ICrudDAL(Of ProductCategory).GetAll
        Using conn As New SqlConnection(GetConnectionString())
            Dim strSql As String = "select * from ProductCategory order by CategoryName"

            Dim results = conn.Query(Of ProductCategory)(strSql)
            Return results
        End Using
    End Function

    Public Function GetById(ByVal id As Integer) As ProductCategory Implements ICrudDAL(Of ProductCategory).GetById
        Using conn As New SqlConnection(GetConnectionString())
            Dim strSql As String = "select * from ProductCategory where CategoryID = @CategoryID"
            Dim param = New With {Key .CategoryID = id}
            Dim result = conn.QuerySingleOrDefault(Of ProductCategory)(strSql, param)
            Return result
        End Using
    End Function



    Public Sub Insert(ByVal entity As ProductCategory) Implements ICrudDAL(Of ProductCategory).Insert
        Using conn As New SqlConnection(GetConnectionString())
            Dim strSql As String = "INSERT INTO ProductCategory (CategoryName) VALUES (@CategoryName)"
            Dim param = New With {Key .CategoryName = entity.CategoryName}
            Try
                Dim result As Integer = conn.Execute(strSql, param)
                If result <> 1 Then
                    Throw New ArgumentException("Insert data failed..")
                End If
            Catch sqlEx As SqlException
                Throw New ArgumentException($"{sqlEx.InnerException.Message} - {sqlEx.Number}")
            Catch ex As Exception
                Throw New ArgumentException(ex.Message)
            End Try
        End Using
    End Sub

    Public Sub Update(ByVal entity As ProductCategory) Implements ICrudDAL(Of ProductCategory).Update
        Using conn As New SqlConnection(GetConnectionString())
            Try
                Dim strSql As String = "UPDATE [ProductCategory] SET [CategoryName] = @CategoryName WHERE [CategoryID] = @CategoryID"
                Dim param = New With {Key .CategoryName = entity.CategoryName, Key .CategoryID = entity.CategoryID}
                Dim result As Integer = conn.Execute(strSql, param)

                ' jika result = -1, berarti update data gagal
                If result <> 1 Then
                    Throw New Exception("Update data failed..")
                End If
            Catch sqlEx As SqlException
                Throw New Exception($"{sqlEx.Message} - {sqlEx.Number}")
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Using
    End Sub

    Public Function GetByName(name As String) As IEnumerable(Of ProductCategory) Implements ICategoryDAL.GetByName
        Using conn As New SqlConnection(GetConnectionString())
            Dim strSql As String = "select * from ProductCategory where CategoryName like @CategoryName"
            Dim param = New With {Key .CategoryName = $"%{name}%"}
            Dim results = conn.Query(Of ProductCategory)(strSql, param)
            Return results
        End Using
    End Function

    Public Function GetWithPaging(pageNumber As Integer, pageSize As Integer, name As String) As IEnumerable(Of ProductCategory) Implements ICategoryDAL.GetWithPaging
        Using conn As New SqlConnection(GetConnectionString())
            Dim strSql As String = "select * from ProductCategory where CategoryName like @CategoryName order by CategoryName OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY"
            Dim param = New With {Key .CategoryName = $"%{name}%", Key .Offset = (pageNumber - 1) * pageSize, Key .PageSize = pageSize}
            Dim results = conn.Query(Of ProductCategory)(strSql, param)
            Return results
        End Using
    End Function

    Public Function GetCountCategories(name As String) As Integer Implements ICategoryDAL.GetCountCategories
        Using conn As New SqlConnection(GetConnectionString())
            Dim strSql As String = "select count(*) from ProductCategory where CategoryName like @CategoryName"
            Dim param = New With {Key .CategoryName = $"%{name}%"}
            Dim result As Integer = Convert.ToInt32(conn.ExecuteScalar(strSql, param))
            Return result
        End Using
    End Function
End Class
