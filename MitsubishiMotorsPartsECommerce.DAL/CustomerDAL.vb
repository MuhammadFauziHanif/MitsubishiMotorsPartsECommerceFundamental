Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports MitsubishiMotorsPartsECommerce.BO
Imports MitsubishiMotorsPartsECommerce.Interface

Public Class CustomerDAL
    Implements ICustomer

    Private strConn As String
    Private conn As SqlConnection
    Private cmd As SqlCommand
    Private dr As SqlDataReader

    Public Sub New()
        conn = New SqlConnection(ConfigurationManager.AppSettings.Get("MyConnectionString"))
    End Sub
    Public Function GetByName() As List(Of Customer) Implements ICustomer.GetByName
        Throw New NotImplementedException()
    End Function

    Public Function Create(obj As Customer) As Integer Implements ICrud(Of Customer).Create
        Try
            Dim strSP = "CreateCustomer"
            cmd = New SqlCommand(strSP, conn)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@FirstName", obj.FirstName)
            cmd.Parameters.AddWithValue("@LastName", obj.LastName)
            cmd.Parameters.AddWithValue("@Email", obj.Email)
            cmd.Parameters.AddWithValue("@PhoneNumber", obj.PhoneNumber)
            cmd.Parameters.AddWithValue("@Address", obj.Address)
            cmd.Parameters.AddWithValue("@Password", obj.Password)

            conn.Open()
            Dim result = cmd.ExecuteNonQuery()
            If result <> 1 Then
                Throw New ArgumentException("Customer not created")
            End If
            Return result
        Catch sqlex As SqlException
            Throw New ArgumentException(sqlex.Message & " " & sqlex.Number)
        Catch ex As Exception
            Throw ex
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Function

    Public Function GetAll() As List(Of Customer) Implements ICrud(Of Customer).GetAll
        Dim Customers As New List(Of Customer)
        Try
            Dim strSql = "SELECT * FROM Customer"

            cmd = New SqlCommand(strSql, conn)
            conn.Open()
            dr = cmd.ExecuteReader()

            If dr.HasRows Then
                While dr.Read()
                    Dim customer As New Customer
                    customer.CustomerID = dr("CustomerID")
                    customer.FirstName = dr("FirstName")
                    customer.LastName = dr("LastName")
                    customer.Email = dr("Email")
                    customer.PhoneNumber = dr("PhoneNumber")
                    customer.Address = dr("Address")
                    customer.PasswordHash = dr("PasswordHash")
                    Customers.Add(customer)
                End While
            End If
            dr.Close()


            Return Customers

        Catch ex As Exception
            Throw ex
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Function

    Public Function GetById(id As Integer) As Customer Implements ICrud(Of Customer).GetById
        Try
            Dim strSP = "GetCustomer"
            cmd = New SqlCommand(strSP, conn)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@CustomerID", id)

            conn.Open()
            dr = cmd.ExecuteReader()
            If dr.HasRows Then
                dr.Read()
                Dim customer As New Customer
                customer.CustomerID = dr("CustomerID")
                customer.FirstName = dr("FirstName")
                customer.LastName = dr("LastName")
                customer.Email = dr("Email")
                customer.PhoneNumber = dr("PhoneNumber")
                customer.Address = dr("Address")
                customer.PasswordHash = dr("PasswordHash")
                Return customer
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
    End Function

    Public Function Update(obj As Customer) As Integer Implements ICrud(Of Customer).Update
        Throw New NotImplementedException()
    End Function

    Public Function Delete(id As Integer) As Integer Implements ICrud(Of Customer).Delete
        Throw New NotImplementedException()
    End Function
End Class
