using System;
using System.Collections.Generic;
using System.Text;

namespace MitsubishiMotorsPartsECommerce.RESTServices.BLL.DTOs
{
    public class AdminDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
