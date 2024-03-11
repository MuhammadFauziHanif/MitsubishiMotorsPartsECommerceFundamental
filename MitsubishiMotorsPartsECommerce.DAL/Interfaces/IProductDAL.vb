Imports MitsubishiMotorsPartsECommerce.BO
Imports System.Collections.Generic

Namespace MyWebFormApp.DAL.Interfaces
    Public Interface IProductDAL
        Inherits ICrudDAL(Of Product)

        Function GetProductWithCategory() As IEnumerable(Of Product)
        Function GetProductByCategory(ByVal categoryId As Integer) As IEnumerable(Of Product)

        Function InsertWithIdentity(ByVal product As Product) As Integer
    End Interface
End Namespace
