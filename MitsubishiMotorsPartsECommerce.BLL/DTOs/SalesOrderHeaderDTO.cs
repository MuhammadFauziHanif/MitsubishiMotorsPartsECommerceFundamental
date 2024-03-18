using System;
using System.Collections.Generic;
using System.Text;

namespace MitsubishiMotorsPartsECommerce.BLL.DTOs
{
    public class SalesOrderHeaderDTO
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }
    }
}
