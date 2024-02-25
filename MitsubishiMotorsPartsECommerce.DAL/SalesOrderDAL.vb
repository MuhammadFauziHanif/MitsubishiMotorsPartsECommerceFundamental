Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports MitsubishiMotorsPartsECommerce.Interface

Public Class SalesOrderDAL
    Implements ISalesOrder

    Private strConn As String
    Private conn As SqlConnection
    Private cmd As SqlCommand
    Private dr As SqlDataReader

    Public Sub New()
        conn = New SqlConnection(ConfigurationManager.AppSettings.Get("MyConnectionString"))
    End Sub
    Public Function Create(customerID As Integer, lstprod As String) As Integer Implements ISalesOrder.Create
        Try
            Dim strSP = "CreateCustomer"
            cmd = New SqlCommand(strSP, conn)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@CustomerID", customerID)
            cmd.Parameters.AddWithValue("@lstprod", lstprod)

            conn.Open()
            Dim result = cmd.ExecuteNonQuery()
            If result <> 1 Then
                Throw New ArgumentException("Sales order not created")
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

End Class
