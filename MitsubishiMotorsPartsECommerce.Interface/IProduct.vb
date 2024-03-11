Imports MitsubishiMotorsPartsECommerce.BO

Public Interface IProduct
    Inherits ICrud(Of Product)
    Function GetByCategory() As IEnumerable(Of Product)


End Interface