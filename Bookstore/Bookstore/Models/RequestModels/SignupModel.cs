﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Models
{
    public class SignupModel
    {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
            public bool IsAdmin { get; set; }
    }
}
