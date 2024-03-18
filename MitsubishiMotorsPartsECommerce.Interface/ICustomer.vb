Imports MitsubishiMotorsPartsECommerce.BO

Public Interface ICustomer
    Inherits ICrud(Of Customer)

    Function GetByName() As List(Of Customer)
    Function Login(Email As String, Password As String) As Customer
End Interface