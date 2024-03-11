using MitsubishiMotorsPartsECommerce.BLL.DTOs;
using MitsubishiMotorsPartsECommerce.BLL.Interfaces;
using MitsubishiMotorsPartsECommerce.DAL;
using MitsubishiMotorsPartsECommerce.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MitsubishiMotorsPartsECommerce.BLL
{
    public class CustomerBLL : ICustomerBLL
    {
        private readonly ICustomer _customerDAL;
        public CustomerBLL()
        {
            _customerDAL = new CustomerDAL();
        }

        public IEnumerable<CustomerDTO> GetAll()
        {
            List<CustomerDTO> listCustomersDto = new List<CustomerDTO>();
            var Customers = _customerDAL.GetAll();
            foreach (var customer in Customers)
            {
                listCustomersDto.Add(new CustomerDTO
                {
                    CustomerID = customer.CustomerID,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber,
                    Address = customer.Address
                });
            }
            return listCustomersDto;
        }
    }
}
