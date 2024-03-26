using MitsubishiMotorsPartsECommerce.Domain.Models;

namespace MitsubishiMotorsPartsECommerce.Data.Interfaces
{
    public interface ICategoryData : ICrudData<ProductCategory>
    {
        Task<IEnumerable<ProductCategory>> GetByName(string name);
        Task<IEnumerable<ProductCategory>> GetWithPaging(int pageNumber, int pageSize, string name);
        Task<int> GetCountCategories(string name);
        Task<int> InsertWithIdentity(ProductCategory category);
    }
}
