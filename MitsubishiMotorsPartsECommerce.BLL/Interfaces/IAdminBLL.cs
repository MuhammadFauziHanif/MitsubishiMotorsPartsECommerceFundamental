using System;
using System.Collections.Generic;
using System.Text;

using MitsubishiMotorsPartsECommerce.BLL.DTOs;

namespace MitsubishiMotorsPartsECommerce.BLL.Interfaces
{
    public interface IAdminBLL
    {
        void ChangePassword(string username, string newPassword);
        void Delete(string username);
        IEnumerable<AdminDTO> GetAll();
        AdminDTO GetByUsername(string username);
        void Insert(AdminCreateDTO entity);
        AdminDTO Login(string username, string password);
    }
}
