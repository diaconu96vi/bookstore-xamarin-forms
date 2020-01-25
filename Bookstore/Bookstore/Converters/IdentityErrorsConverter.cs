using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Converters
{
    public static class IdentityErrorsConverter
    {
        public static string IdentityErrorsToString(List<IdentityError> identityErrors )
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach(var error in identityErrors)
            {
                stringBuilder.AppendLine(error.Description);
            }
            return stringBuilder.ToString();
        }
    }
}
