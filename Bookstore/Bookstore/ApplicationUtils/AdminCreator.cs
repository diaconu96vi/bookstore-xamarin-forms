using Bookstore.Enums;
using Bookstore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.ApplicationUtils
{
    public static class AdminCreator
    {
        public static void CreateAdmin(SignupModel signupModel)
        {
            if (signupModel != null && signupModel.UserName.Contains(Enum.GetName(typeof(UserTypeEnum), UserTypeEnum.Admin)))
            {
                signupModel.IsAdmin = true;
            }
        }
    }
}
