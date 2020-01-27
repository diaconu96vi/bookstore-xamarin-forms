using Bookstore.ApplicationUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.ViewModels.TabbedPages
{
    public class ProfilePageViewModel : BaseViewModel
    {
        public bool? IsAdmin { get; set; }

        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public ProfilePageViewModel()
        {
            UserName = ApplicationGeneralSettings.CurrentUser.UserName;
            UserEmail = ApplicationGeneralSettings.CurrentUser.Email;
            OnPropertyChanged(nameof(UserName));
            OnPropertyChanged(nameof(UserEmail));
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
