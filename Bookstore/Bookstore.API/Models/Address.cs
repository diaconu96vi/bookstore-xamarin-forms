using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Models
{
    public class Address
    {
        [Required]
        [Key]
        public int AddressSysID { get; set; }
        [ForeignKey("AppUser")]
        public string AppUserFK_SysID { get; set; }
        public string AddressTitle { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string FullAddress { get; set; }

        //Reverse navigation
        public AppUser AppUser { get; set; }
    }
}
