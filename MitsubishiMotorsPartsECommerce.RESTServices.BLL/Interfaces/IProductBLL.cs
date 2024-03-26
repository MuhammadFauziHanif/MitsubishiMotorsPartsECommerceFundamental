using System;
using System.Collections.Generic;
using System.Text;

using MitsubishiMotorsPartsECommerce.RESTServices.BLL.DTOs;

namespace MitsubishiMotorsPartsECommerce.RESTServices.BLL.Interfaces
{
    public interface IProductBLL
    {
        Task<ProductDTO> Insert(ProductCreateDTO entity);
        Task<IEnumerable<ProductDTO>> GetProductWithCategory();
        Task<IEnumerable<ProductDTO>> GetProductByCategory(int categoryId);
        Task<IEnumerable<ProductDTO>> GetAll();
        Task<ProductDTO> GetById(int id);

        Task<int> InsertWithIdentity(ProductCreateDTO entity);
        Task<ProductDTO> Update(int id, ProductUpdateDTO entity);
        Task<bool> Delete(int id);
    }
}
