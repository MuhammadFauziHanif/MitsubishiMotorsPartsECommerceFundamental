using System;
using System.Collections.Generic;
using System.Text;

namespace MitsubishiMotorsPartsECommerce.RESTServices.BLL.DTOs
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; }

        public CategoryDTO Category { get; set; }
    }
}
