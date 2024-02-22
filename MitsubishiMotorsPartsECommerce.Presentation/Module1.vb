Imports MitsubishiMotorsPartsECommerce.BO
Imports MitsubishiMotorsPartsECommerce.DAL

Module Module1

    Sub GetAllCustomer()
        Dim customerDAL As New DAL.CustomerDAL

        Dim customers = customerDAL.GetAll()
        For Each customer As Customer In customers
            Console.WriteLine("{0}-{1}-{2}", customer.CustomerID, customer.FirstName, customer.Address)
        Next

    End Sub

    Sub CreateCustomer()
        Dim customerDAL As New DAL.CustomerDAL
        Dim customer As New Customer
        customer.FirstName = "Muhammad"
        customer.LastName = "Hanif"
        customer.Email = "muhammad.hanif010@bsi.co.id"
        customer.PhoneNumber = "08123456789"
        customer.Address = "Indonesia"
        customer.Password = "Password"
        customerDAL.Create(customer)
    End Sub

    Sub Main()
        GetAllCustomer()
    End Sub

End Module
