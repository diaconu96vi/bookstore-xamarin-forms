using Bookstore.ApplicationUtils;
using Bookstore.Converters;
using Bookstore.Models.RequestModels;
using Bookstore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bookstore.ViewModels.ProfileDetails
{
    public class ChangePasswordPageViewModel
    {
        public Command ChangePasswordCommand { get; set; }

        public string OldPasswordEntry { get; set; }

        public string NewPasswordEntry { get; set; }

        public string ConfirmNewPasswordEntry { get; set; }

        private AccountApiService _apiService;

        public ChangePasswordPageViewModel()
        {
            _apiService = new AccountApiService();
            ChangePasswordCommand = new Command(async () => await ExecuteChangePasswordCommand());
        }

        public async Task ExecuteChangePasswordCommand()
        {
            if (!string.Equals(NewPasswordEntry, ConfirmNewPasswordEntry))
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "New Password confirmation is diffrenet;", "Cancel");
            }
            else
            {
                var model = new ChangePasswordModel()
                {
                    Email = ApplicationGeneralSettings.CurrentUser.Email,
                    NewPassword = NewPasswordEntry,
                    OldPassword = OldPasswordEntry
                };
                var result = await _apiService.ChangePassword(model);

                if (result.Item1 != null)
                {
                    await Application.Current.MainPage.Navigation.PopModalAsync(true);
                }
                else
                {
                    if (result.Item2 != null && result.Item2.Any())
                    {
                        var errorsString = IdentityErrorsConverter.IdentityErrorsToString(result.Item2);
                        await Application.Current.MainPage.DisplayAlert("Warning", errorsString, "Cancel");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Warning", "Something went wrong", "Cancel");
                    }
                }
            }
        }
    }
}
