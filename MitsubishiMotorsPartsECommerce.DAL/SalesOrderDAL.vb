Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports MitsubishiMotorsPartsECommerce.BO
Imports MitsubishiMotorsPartsECommerce.Interface

Public Class SalesOrderDAL
    Implements ISalesOrderDAL

    Private strConn As String
    Private conn As SqlConnection
    Private cmd As SqlCommand
    Private dr As SqlDataReader

    Private Function GetConnectionString() As String
        Return ConfigurationManager.ConnectionStrings("MyDbConnectionString").ConnectionString
    End Function

    Public Function Create(customerID As Integer, lstprod As String) As Integer Implements ISalesOrderDAL.Create
        Using conn As New SqlConnection(GetConnectionString())
            Try
                Dim strSP = "SalesOrder"
                cmd = New SqlCommand(strSP, conn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@CustomerID", customerID)
                cmd.Parameters.AddWithValue("@lstprod", lstprod)

                conn.Open()
                Dim result = cmd.ExecuteNonQuery()
                Return result
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

    Public Function GetRevenueByCategory() As List(Of CategorySalesView) Implements ISalesOrderDAL.GetRevenueByCategory
        Dim CategorySales As New List(Of CategorySalesView)
        Try
            Dim strSP = "CategorySalesView"
            cmd = New SqlCommand(strSP, conn)
            cmd.CommandType = CommandType.StoredProcedure
            conn.Open()
            dr = cmd.ExecuteReader()
            If dr.HasRows Then
                While dr.Read
                    Dim categorySale As New CategorySalesView
                    categorySale.CategoryName = dr("Category").ToString
                    categorySale.TotalSalesAmount = dr("Revenue").ToString
                    CategorySales.Add(categorySale)
                End While
            End If
            dr.Close()

            Return CategorySales

        Catch sqlex As SqlException
            Throw New ArgumentException(sqlex.Message & " " & sqlex.Number)
        Catch ex As Exception
            Throw ex
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Function

    Public Function GetRevenueByMonth() As List(Of MonthSalesView) Implements ISalesOrderDAL.GetRevenueByMonth
        Dim TotalRevenuePerMonths As New List(Of MonthSalesView)
        Try
            Dim strSP = "TotalRevenuePerMonth"
            cmd = New SqlCommand(strSP, conn)
            cmd.CommandType = CommandType.StoredProcedure
            conn.Open()
            dr = cmd.ExecuteReader()
            If dr.HasRows Then
                While dr.Read
                    Dim revenuePerMonth As New MonthSalesView

                    revenuePerMonth.OrderYear = CInt(dr("OrderYear"))
                    revenuePerMonth.OrderMonth = CInt(dr("OrderMonth"))
                    revenuePerMonth.TotalRevenue = CDec(dr("TotalRevenue"))
                    TotalRevenuePerMonths.Add(revenuePerMonth)

                End While
            End If
            dr.Close()

            Return TotalRevenuePerMonths

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
