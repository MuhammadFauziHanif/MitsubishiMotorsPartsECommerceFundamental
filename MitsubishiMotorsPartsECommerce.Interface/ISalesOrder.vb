Imports MitsubishiMotorsPartsECommerce.BO

Public Interface ISalesOrder
    Function Create(customerID As Integer, lstprod As String) As Integer

    Function GetRevenueByCategory() As List(Of CategorySalesView)

    Function GetRevenueByMonth() As List(Of MonthSalesView)
End Interface
