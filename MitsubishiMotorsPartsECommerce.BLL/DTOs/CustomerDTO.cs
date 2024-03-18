using System;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace MitsubishiMotorsPartsECommerce.BLL.DTOs
{
    public class CustomerDTO
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullNameWithID { get { return $"{CustomerID} - {FirstName} {LastName} - {Address}"; } }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
