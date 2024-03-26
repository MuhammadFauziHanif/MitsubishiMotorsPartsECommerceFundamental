using System;
using System.Collections.Generic;
using System.Text;

namespace MitsubishiMotorsPartsECommerce.RESTServices.BLL.DTOs
{
    public class SalesOrderCreateDTO
    {
        public int CustomerID { get; set; }
        public string LstProd { get; set; }
    }
}
