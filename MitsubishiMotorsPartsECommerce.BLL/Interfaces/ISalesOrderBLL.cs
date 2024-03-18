using MitsubishiMotorsPartsECommerce.BLL.DTOs;
using MitsubishiMotorsPartsECommerce.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MitsubishiMotorsPartsECommerce.BLL.Interfaces
{
    public interface ISalesOrderBLL
    {
        int Create(SalesOrderCreateDTO salesOrderCreate);
        IEnumerable<SalesOrderHeaderDTO> GetSalesOrderHeaderByCustomerID(int customerID);
    }
}
