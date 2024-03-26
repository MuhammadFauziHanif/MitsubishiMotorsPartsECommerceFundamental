using MitsubishiMotorsPartsECommerce.Domain.Models;

namespace MitsubishiMotorsPartsECommerce.Data.Interfaces
{
    public interface IAdminData : ICrudData<Admin>
    {
        Task<IEnumerable<Admin>> GetAllWithRoles();
        Task<Admin> GetAdminWithRoles(string username);
        Task<Admin> GetByUsername(string username);
        Task<Admin> Login(string username, string password);
        Task<Task> ChangePassword(string username, string newPassword);
    }
}
