Public Interface ICrudDAL(Of T)
    Function GetAll() As IEnumerable(Of T)
    Function GetById(ByVal id As Integer) As T
    Sub Insert(ByVal entity As T)
    Sub Update(ByVal entity As T)
    Sub Delete(ByVal id As Integer)
End Interface
