using Bookstore.Converters;
using Bookstore.Models;
using Bookstore.Services;
using Bookstore.Views;
using Bookstore.Views.TabbedPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bookstore.ViewModels.Startup
{
    public class SignupPageViewModel : BaseViewModel
    {
        public Command RegisterCommand { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        AccountApiService _apiServices = new AccountApiService();

        public SignupPageViewModel()
        {            
            RegisterCommand = new Command(async () => await ExecuteRegisterCommand());
        }

        public async Task ExecuteRegisterCommand()
        {
            if(CheckValuesEmpty())
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Values are null", "Cancel");
                return;
            }
            if(!IsValidEmail(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Warning", "Email not valid", "Cancel");
                return;
            }
            var model = new SignupModel()
            {
                UserName = UserName,
                Email = Email,
                Password = Password,
                ConfirmPassword = ConfirmPassword,
                IsAdmin = false
            };
            var isSuccess = await _apiServices.RegisterAsync(model);    
            if(isSuccess.Item1)
            {
                
                await Application.Current.MainPage.Navigation.PushAsync(new ParentTabbedPage());
            }
            else
            {
                if (isSuccess.Item2 != null && isSuccess.Item2.Any())
                {
                    var errorsString = IdentityErrorsConverter.IdentityErrorsToString(isSuccess.Item2);
                    await Application.Current.MainPage.DisplayAlert("Warning", errorsString, "Cancel");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Warning", "Something went wrong", "Cancel");
                }
            }
        }  
        
        private bool CheckValuesEmpty()
        {
            if(string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                return true;
            }
            return false;
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
