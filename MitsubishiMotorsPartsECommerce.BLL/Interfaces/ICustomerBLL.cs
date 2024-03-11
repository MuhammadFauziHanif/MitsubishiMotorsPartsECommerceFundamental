using MitsubishiMotorsPartsECommerce.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MitsubishiMotorsPartsECommerce.BLL.Interfaces
{
    public interface ICustomerBLL
    {
        IEnumerable<CustomerDTO> GetAll();
    }
}
