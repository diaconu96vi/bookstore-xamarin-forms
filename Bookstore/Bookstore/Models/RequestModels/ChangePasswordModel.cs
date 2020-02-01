using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Models.RequestModels
{
    public class ChangePasswordModel
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
