﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MitsubishiMotorsPartsECommerce.BLL.DTOs
{
    public class AdminDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
