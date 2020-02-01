using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Models.RequestModels
{
    public class UserDetailsModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string OldEmail { get; set; }
    }
}
