using Bookstore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.ApplicationUtils
{
    public static class ApplicationGeneralSettings
    {
        public static string ApiUrl => "http://localhost:5005/api/";

        public static AppUser CurrentUser { get; set; } 
    }
}
