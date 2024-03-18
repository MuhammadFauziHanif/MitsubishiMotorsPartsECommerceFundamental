using MitsubishiMotorsPartsECommerce.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MitsubishiMotorsPartsECommerce.BLL.Interfaces
{
    public interface ICustomerBLL
    {
        IEnumerable<CustomerDTO> GetAll();
        CustomerDTO Login(CustomerLoginDTO customerLoginDTO);
        void Insert(CustomerCreateDTO customerCreateDTO);
    }
}
