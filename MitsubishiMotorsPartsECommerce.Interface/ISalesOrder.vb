﻿Imports MitsubishiMotorsPartsECommerce.BO

Public Interface ISalesOrderDAL
    Function Create(customerID As Integer, lstprod As String) As Integer

    Function GetRevenueByCategory() As List(Of CategorySalesView)

    Function GetRevenueByMonth() As List(Of MonthSalesView)

    Function GetSalesOrderHeaderByCustomerID(customerID As Integer) As List(Of OrderHeader)
End Interface
