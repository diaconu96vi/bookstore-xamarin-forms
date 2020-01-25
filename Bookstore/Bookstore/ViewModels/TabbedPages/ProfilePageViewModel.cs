using Bookstore.ApplicationUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.ViewModels.TabbedPages
{
    public class ProfilePageViewModel : BaseViewModel
    {
        public bool? IsAdmin { get; set; }

        public ProfilePageViewModel()
        {
            VerifyAdminPage();
        }

        private void VerifyAdminPage()
        {
            if(ApplicationGeneralSettings.CurrentUser != null)
            {
                IsAdmin = ApplicationGeneralSettings.CurrentUser.IsAdmin;
            }
            else
            {
                IsAdmin = false;
            }
        }
    }
}
