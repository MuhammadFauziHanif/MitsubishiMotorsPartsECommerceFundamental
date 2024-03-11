
using MitsubishiMotorsPartsECommerce.BLL.DTOs;
using MitsubishiMotorsPartsECommerce.BLL.Interfaces;
using MitsubishiMotorsPartsECommerce.BO;
using MitsubishiMotorsPartsECommerce.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace MitsubishiMotorsPartsECommerce.BLL
{
    public class AdminBLL : IAdminBLL
    {
        private readonly IAdminDAL _adminDAL;

        public AdminBLL()
        {
            _adminDAL = new AdminDAL();
        }

        public void ChangePassword(string username, string newPassword)
        {
            throw new NotImplementedException();
        }

        public void Delete(string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AdminDTO> GetAll()
        {
            var users = _adminDAL.GetAll();
            var adminsDTO = new List<AdminDTO>();
            foreach (var user in users)
            {
                adminsDTO.Add(new AdminDTO
                {
                    Username = user.Username,
                    Email = user.Email,
                    Role = user.Role,
                });
            }
            return adminsDTO;
        }

        public AdminDTO GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public void Insert(AdminCreateDTO entity)
        {
            if (string.IsNullOrEmpty(entity.Username))
            {
                throw new ArgumentException("Username is required");
            }
            if (string.IsNullOrEmpty(entity.Password))
            {
                throw new ArgumentException("Password is required");
            }
            if (string.IsNullOrEmpty(entity.Email))
            {
                throw new ArgumentException("Email is required");
            }

            try
            {
                var newAdmin = new Admin
                {
                    Username = entity.Username,
                    Password = Helper.GetHash(entity.Password),
                    Email = entity.Email,
                    Role = entity.Role
                };
                _adminDAL.Insert(newAdmin);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("2627"))
                {
                    throw new ArgumentException("Username already exists");
                }

                throw new ArgumentException(ex.Message);
            }
        }

        public AdminDTO Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username is required");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password is required");
            }
            try
            {
                var result = _adminDAL.Login(username, Helper.GetHash(password));
                if (result == null)
                {
                    throw new ArgumentException("Username or Password is wrong");
                }

                AdminDTO adminDTO = new AdminDTO
                {
                    Username = result.Username,
                    Email = result.Email,
                    Role = result.Role
                };

                return adminDTO;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
