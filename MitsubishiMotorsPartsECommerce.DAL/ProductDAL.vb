Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports Dapper
Imports MitsubishiMotorsPartsECommerce.BO
Imports MitsubishiMotorsPartsECommerce.DAL.MyWebFormApp.DAL.Interfaces
Imports MitsubishiMotorsPartsECommerce.Interface

Public Class ProductDAL
    Implements IProductDAL

    Private strConn As String
    Private conn As SqlConnection
    Private cmd As SqlCommand
    Private dr As SqlDataReader

    Private Function GetConnectionString() As String
        ' Return @"Data Source=ACTUAL;Initial Catalog=LatihanDb;Integrated Security=True;TrustServerCertificate=True"
        Return ConfigurationManager.ConnectionStrings("MyDbConnectionString").ConnectionString
    End Function

    Public Sub Insert(entity As Product) Implements ICrudDAL(Of Product).Insert
        Using conn As New SqlConnection(GetConnectionString())
            Try
                Dim strSP = "CreateProduct"
                cmd = New SqlCommand(strSP, conn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@ProductName", entity.ProductName)
                cmd.Parameters.AddWithValue("@CategoryID", entity.CategoryID)
                cmd.Parameters.AddWithValue("@Description", entity.Description)
                cmd.Parameters.AddWithValue("@Price", entity.Price)
                cmd.Parameters.AddWithValue("@StockQuantity", entity.StockQuantity)
                cmd.Parameters.AddWithValue("@ImageUrl", entity.ImageUrl)

                conn.Open()
                Dim result = cmd.ExecuteNonQuery()
                If result <> 1 Then
                    Throw New ArgumentException("Product not created")
                End If
            Catch sqlex As SqlException
                Throw New ArgumentException(sqlex.Message & " " & sqlex.Number)
            Catch ex As Exception
                Throw ex
            Finally
                cmd.Dispose()
                conn.Close()
            End Try
        End Using

    End Sub

    Public Sub Update(entity As Product) Implements ICrudDAL(Of Product).Update
        Using conn As New SqlConnection(GetConnectionString())
            Try
                Dim strSP = "UpdateProduct"
                cmd = New SqlCommand(strSP, conn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@ProductID", entity.ProductID)
                cmd.Parameters.AddWithValue("@ProductName", entity.ProductName)
                cmd.Parameters.AddWithValue("@CategoryID", entity.CategoryID)
                cmd.Parameters.AddWithValue("@Description", entity.Description)
                cmd.Parameters.AddWithValue("@Price", entity.Price)
                cmd.Parameters.AddWithValue("@StockQuantity", entity.StockQuantity)
                cmd.Parameters.AddWithValue("@ImageUrl", entity.ImageUrl)

                conn.Open()
                Dim result = cmd.ExecuteNonQuery()
            Catch sqlex As SqlException
                Throw New ArgumentException(sqlex.Message & " " & sqlex.Number)
            Catch ex As Exception
                Throw ex
            Finally
                cmd.Dispose()
                conn.Close()
            End Try
        End Using
    End Sub


    Public Function GetById(id As Integer) As Product Implements ICrudDAL(Of Product).GetById
        Using conn As New SqlConnection(GetConnectionString())
            Try
                Dim strSP = "GetProduct"
                cmd = New SqlCommand(strSP, conn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@ProductID", id)

                conn.Open()
                dr = cmd.ExecuteReader()
                If dr.HasRows Then
                    dr.Read()
                    Dim product As New Product
                    product.ProductID = dr("ProductID")
                    product.ProductName = dr("ProductName")
                    product.CategoryID = dr("CategoryID")
                    product.Description = dr("Description")
                    product.Price = dr("Price")
                    product.StockQuantity = dr("StockQuantity")
                    product.ImageUrl = dr("ImageUrl")

                    Return product
                Else
                    Return Nothing
                End If
            Catch sqlex As SqlException
                Throw New ArgumentException(sqlex.Message & " " & sqlex.Number)
            Catch ex As Exception
                Throw ex
            Finally
                cmd.Dispose()
                conn.Close()
            End Try
        End Using
    End Function


    Public Sub Delete(id As Integer) Implements ICrudDAL(Of Product).Delete
        Using conn As New SqlConnection(GetConnectionString())
            Try
                Dim strSP = "DeleteProduct"
                cmd = New SqlCommand(strSP, conn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@ProductID", id)

                conn.Open()
                Dim result = cmd.ExecuteNonQuery()
            Catch sqlex As SqlException
                Throw New ArgumentException(sqlex.Message & " " & sqlex.Number)
            Catch ex As Exception
                Throw ex
            Finally
                cmd.Dispose()
                conn.Close()
            End Try
        End Using
    End Sub

    Public Function GetProductWithCategory() As IEnumerable(Of Product) Implements IProductDAL.GetProductWithCategory
        Throw New NotImplementedException()
    End Function

    Public Function GetProductByCategory(categoryId As Integer) As IEnumerable(Of Product) Implements IProductDAL.GetProductByCategory
        Using conn As New SqlConnection(GetConnectionString())
            Dim strSql As String = "SELECT p.*, pc.* FROM Product p JOIN ProductCategory pc ON p.CategoryID = pc.CategoryID WHERE p.CategoryID = @CategoryID"
            Dim param = New With {Key .CategoryID = categoryId}
            Dim results = conn.Query(Of Product, ProductCategory, Product)(strSql, Function(product, category)
                                                                                       product.Category = category
                                                                                       Return product
                                                                                   End Function, param, splitOn:="CategoryID")
            Return results
        End Using
    End Function


    Public Function GetAll() As IEnumerable(Of Product) Implements ICrudDAL(Of Product).GetAll
        Using conn As New SqlConnection(GetConnectionString())
            Dim strSql As String = "SELECT * FROM Product"
            Dim results = conn.Query(Of Product)(strSql)
            Return results
        End Using
    End Function

    Public Function InsertWithIdentity(product As Product) As Integer Implements IProductDAL.InsertWithIdentity
        Using conn As New SqlConnection(GetConnectionString())
            Dim strSql As String = "INSERT INTO Product(ProductName, CategoryID, Description, Price, StockQuantity, ImageUrl) VALUES(@ProductName, @CategoryID, @Description, @Price, @StockQuantity, @ImageUrl); SELECT @@IDENTITY"
            Dim param = New With {
                Key .ProductName = product.ProductName,
                Key .CategoryID = product.CategoryID,
                Key .Description = product.Description,
                Key .Price = product.Price,
                Key .StockQuantity = product.StockQuantity,
                Key .ImageUrl = product.ImageUrl
            }
            Try
                Dim result = Convert.ToInt32(conn.ExecuteScalar(strSql, param))
                Return result
            Catch sqlEx As SqlException
                Throw New ArgumentException($"{sqlEx.InnerException.Message} - {sqlEx.Number}")
            Catch ex As Exception
                Throw New ArgumentException(ex.Message)
            End Try
        End Using
    End Function
End Class
