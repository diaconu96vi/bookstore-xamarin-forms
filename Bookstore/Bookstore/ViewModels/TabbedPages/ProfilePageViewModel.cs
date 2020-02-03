﻿using Bookstore.ApplicationUtils;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Bookstore.ViewModels.TabbedPages
{
    public class ProfilePageViewModel : BaseViewModel
    {
        public ImageSource UserImage { get; set; }
        public bool? IsAdmin { get; set; }

        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public bool ChangeDetailsVisible { get; set; }

        public ProfilePageViewModel()
        {
            UserName = ApplicationGeneralSettings.CurrentUser.UserName;
            UserEmail = ApplicationGeneralSettings.CurrentUser.Email;
            OnPropertyChanged(nameof(UserName));
            OnPropertyChanged(nameof(UserEmail));
            VerifyAdminPage();
            UpdateFacebookDetails();
        }
        private void UpdateFacebookDetails()
        {
            if(ApplicationGeneralSettings.FacebookUser != null)
            {
                UserImage = ApplicationGeneralSettings.FacebookUser.Picture.Data.Url;
                ChangeDetailsVisible = false;
                UserName = ApplicationGeneralSettings.FacebookUser.Name;
            }
            else
            {
                UserImage = "bookstorelogo.png";
                ChangeDetailsVisible = true;
            }
            OnPropertyChanged(nameof(UserImage));
            OnPropertyChanged(nameof(ChangeDetailsVisible));
            OnPropertyChanged(nameof(UserName));
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
