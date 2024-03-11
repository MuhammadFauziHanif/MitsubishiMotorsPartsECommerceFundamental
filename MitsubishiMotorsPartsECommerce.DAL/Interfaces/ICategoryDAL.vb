Imports MitsubishiMotorsPartsECommerce.BO

Public Interface ICategoryDAL
    Inherits ICrudDAL(Of ProductCategory)
    Function GetByName(name As String) As IEnumerable(Of ProductCategory)
    Function GetWithPaging(pageNumber As Integer, pageSize As Integer, name As String) As IEnumerable(Of ProductCategory)
    Function GetCountCategories(name As String) As Integer
End Interface
