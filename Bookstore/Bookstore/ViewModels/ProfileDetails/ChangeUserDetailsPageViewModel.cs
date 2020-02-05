using Bookstore.ApplicationUtils;
using Bookstore.Models;
using Bookstore.Models.RequestModels;
using Bookstore.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bookstore.ViewModels.ProfileDetails
{
    public class ChangeUserDetailsPageViewModel
    {
        public Command SaveChangesCommand { get; set; }

        public string UserNameEntry { get; set; }
        public string EmailEntry { get; set; }

        private AccountApiService _apiService;

        public ChangeUserDetailsPageViewModel()
        {
            UserNameEntry = ApplicationGeneralSettings.CurrentUser.UserName;
            EmailEntry = ApplicationGeneralSettings.CurrentUser.Email;

            _apiService = new AccountApiService();
            SaveChangesCommand = new Command(async () => await ExecuteSaveChangesCommand());
        }

        public async Task ExecuteSaveChangesCommand()
        {
            if (string.Equals(EmailEntry, ApplicationGeneralSettings.CurrentUser.Email) &&
                string.Equals(UserNameEntry, ApplicationGeneralSettings.CurrentUser.UserName))
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "No changes have been performed", "Cancel");
            }
            else
            {
                var model = new UserDetailsModel()
                {
                    Email = EmailEntry,
                    UserName = UserNameEntry,
                    OldEmail = ApplicationGeneralSettings.CurrentUser.Email
                };

                bool success = await _apiService.ChangeUserDetails(model);

                if (!success)
                    await Application.Current.MainPage.DisplayAlert("Warning", "Changes could not be performed performed", "Cancel");
            }
            ApplicationGeneralSettings.CurrentUser.Email = EmailEntry;
            ApplicationGeneralSettings.CurrentUser.UserName = UserNameEntry;
            MessagingCenter.Send<AppUser>(new AppUser(), "RefreshProfile");
            await Application.Current.MainPage.Navigation.PopModalAsync(true);
        }
    }
}
