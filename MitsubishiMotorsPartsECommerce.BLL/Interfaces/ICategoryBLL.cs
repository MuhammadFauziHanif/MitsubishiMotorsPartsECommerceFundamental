using MitsubishiMotorsPartsECommerce.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MitsubishiMotorsPartsECommerce.BLL.Interfaces
{
    internal interface ICategoryBLL
    {
        void Delete(int id);
        IEnumerable<CategoryDTO> GetAll();
        CategoryDTO GetById(int id);
        IEnumerable<CategoryDTO> GetByName(string name);
        void Insert(CategoryCreateDTO entity);
        void Update(CategoryUpdateDTO entity);
        IEnumerable<CategoryDTO> GetWithPaging(int pageNumber, int pageSize, string name);
        int GetCountCategories(string name);
    }
}
