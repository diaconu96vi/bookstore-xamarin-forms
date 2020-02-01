using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Models
{
    public class Address
    {
        public int AddressSysID { get; set; }
        public string AppUserFK_SysID { get; set; }
        public string AddressTitle { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string FullAddress { get; set; }
    }
}
