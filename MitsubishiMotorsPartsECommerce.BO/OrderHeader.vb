Public Class OrderHeader
    Public Sub New()
        Me.OrderDetails = New List(Of OrderDetail)()
        Me.Payments = New Payment()
    End Sub

    Public Property OrderID As Integer
    Public Property CustomerID As Integer
    Public Property OrderDate As DateTime
    Public Property TotalAmount As Decimal
    Public Property OrderStatus As String

    Public Property Customer As Customer
    Public Property OrderDetails As IEnumerable(Of OrderDetail)
    Public Property Payments As Payment
End Class
