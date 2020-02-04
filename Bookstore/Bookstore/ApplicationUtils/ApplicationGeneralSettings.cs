using Bookstore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.ApplicationUtils
{
    public static class ApplicationGeneralSettings
    {
        public static string ApiUrl => "http://192.168.1.104:44301/api/";

        public static AppUser CurrentUser { get; set; } 

        public static FacebookEmail FacebookUser { get; set; } 

        public static string Token { get { return "gwYRXGjqZA0!"; } }
    }
}
