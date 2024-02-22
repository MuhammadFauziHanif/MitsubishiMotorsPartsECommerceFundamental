Imports MitsubishiMotorsPartsECommerce.BO
Imports MitsubishiMotorsPartsECommerce.DAL

Module Module1

    Sub SampleDb()
        Dim customerDAL As New CustomerDAL

        Dim customers = customerDAL.GetAll()
        For Each customer As Customer In customers
            Console.WriteLine("{0}-{1}-{2}", customer.CustomerID, customer.FirstName, customer.Address)
        Next


    End Sub

    Sub Main()

    End Sub

End Module
