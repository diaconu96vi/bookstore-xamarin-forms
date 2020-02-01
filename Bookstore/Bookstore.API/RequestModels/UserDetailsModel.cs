using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.RequestModels
{
    public class UserDetailsModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string OldEmail { get; set; }
    }
}
