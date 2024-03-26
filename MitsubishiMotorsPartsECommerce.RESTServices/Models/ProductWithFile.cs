namespace MitsubishiMotorsPartsECommerce.RESTServices.Models
{
    public class ProductWithFile
    {
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public IFormFile? file { get; set; }
    }
}
