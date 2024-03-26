namespace MitsubishiMotorsPartsECommerce.RESTServices.BLL.DTOs
{
    public class ProductCreateDTO
    {
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; }
    }
}