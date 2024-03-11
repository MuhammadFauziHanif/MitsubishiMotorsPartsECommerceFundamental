using System;
using System.Collections.Generic;
using System.Text;

using MitsubishiMotorsPartsECommerce.BLL.DTOs;

namespace MitsubishiMotorsPartsECommerce.BLL.Interfaces
{
    public interface IProductBLL
    {
        void Insert(ProductCreateDTO entity);
        IEnumerable<ProductDTO> GetProductWithCategory();
        IEnumerable<ProductDTO> GetProductByCategory(int categoryId);
        IEnumerable<ProductDTO> GetAll();

        int InsertWithIdentity(ProductCreateDTO entity);
        void Update(ProductUpdateDTO entity);
        void Delete(int id);

    }
}
