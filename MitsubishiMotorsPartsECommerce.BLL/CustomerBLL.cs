using MitsubishiMotorsPartsECommerce.BLL.DTOs;
using MitsubishiMotorsPartsECommerce.BLL.Interfaces;
using MitsubishiMotorsPartsECommerce.BO;
using MitsubishiMotorsPartsECommerce.DAL;
using MitsubishiMotorsPartsECommerce.DAL.MitsubishiMotorsPartsECommerce.DAL;
using MitsubishiMotorsPartsECommerce.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using static Dapper.SqlMapper;

namespace MitsubishiMotorsPartsECommerce.BLL
{
    public class CustomerBLL : ICustomerBLL
    {
        private readonly ICustomer _customerDAL;
        public CustomerBLL()
        {
            _customerDAL = new CustomerDAL();
        }

        public IEnumerable<CustomerDTO> GetAll()
        {
            List<CustomerDTO> listCustomersDto = new List<CustomerDTO>();
            var Customers = _customerDAL.GetAll();
            foreach (var customer in Customers)
            {
                listCustomersDto.Add(new CustomerDTO
                {
                    CustomerID = customer.CustomerID,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber,
                    Address = customer.Address
                });
            }
            return listCustomersDto;
        }

        public void Insert(CustomerCreateDTO customerCreateDTO)
        {
            if (string.IsNullOrEmpty(customerCreateDTO.Password))
            {
                throw new ArgumentException("Password is required");
            }
            if (string.IsNullOrEmpty(customerCreateDTO.FirstName))
            {
                throw new ArgumentException("First Name is required");
            }
            if (string.IsNullOrEmpty(customerCreateDTO.LastName))
            {
                throw new ArgumentException("Last Name is required");
            }
            if (string.IsNullOrEmpty(customerCreateDTO.Address))
            {
                throw new ArgumentException("Address is required");
            }
            if (string.IsNullOrEmpty(customerCreateDTO.Email))
            {
                throw new ArgumentException("Email is required");
            }
            if (string.IsNullOrEmpty(customerCreateDTO.PhoneNumber))
            {
                throw new ArgumentException("Phone Number is required");
            }
            if (customerCreateDTO.Password != customerCreateDTO.Repassword)
            {
                throw new ArgumentException("Password and Re-Password must be same");
            }

            try
            {
                var newCustomer = new Customer
                {
                    Password = Helper.GetHash(customerCreateDTO.Password),
                    FirstName = customerCreateDTO.FirstName,
                    LastName = customerCreateDTO.LastName,
                    Address = customerCreateDTO.Address,
                    Email = customerCreateDTO.Email,
                    PhoneNumber = customerCreateDTO.PhoneNumber
                };
                _customerDAL.Create(newCustomer);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public CustomerDTO Login(CustomerLoginDTO customerLoginDTO)
        {
            if (string.IsNullOrEmpty(customerLoginDTO.Email))
            {
                throw new ArgumentException("Email is required");
            }
            if (string.IsNullOrEmpty(customerLoginDTO.Password))
            {
                throw new ArgumentException("Password is required");
            }
            try
            {
                var result = _customerDAL.Login(customerLoginDTO.Email, Helper.GetHash(customerLoginDTO.Password));
                if (result == null)
                {
                    throw new ArgumentException("Email or Password is wrong");
                }

                CustomerDTO customerDTO = new CustomerDTO
                {
                    CustomerID = result.CustomerID,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Email = result.Email,
                    PhoneNumber = result.PhoneNumber,
                    Address = result.Address
                };

                return customerDTO;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
