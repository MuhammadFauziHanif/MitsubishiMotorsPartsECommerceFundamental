using MitsubishiMotorsPartsECommerce.Domain.Models;

namespace MitsubishiMotorsPartsECommerce.Data.Interfaces
{
    public interface IProductData : ICrudData<Product>
    {
        Task<IEnumerable<Product>> GetProductWithCategory();
        Task<IEnumerable<Product>> GetProductByCategory(int categoryId);
        Task<IEnumerable<Product>> GetWithPaging(int categoryId, int pageNumber, string name);

        Task<int> GetCountProducts(string name);
        Task<int> InsertWithIdentity(Product product);

        Task<Task> InsertProductWithCategory(Product product);
    }
}
