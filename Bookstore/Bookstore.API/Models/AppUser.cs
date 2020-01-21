using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
        }


        public string Address { get; set; }

        public string City { get; set; }

        public int PostalCode { get; set; }

        public Nullable<bool> ActiveUser { get; set; }

        public Nullable<bool> IsAdmin { get; set; }
        public Nullable<DateTime> DtCreated { get; set; }

        public Nullable<DateTime> DtModified { get; set; }

        public byte[] ImageSource { get; set; }

    }
}
