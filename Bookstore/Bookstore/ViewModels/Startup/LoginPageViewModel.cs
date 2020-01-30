using Bookstore.ApplicationUtils;
using Bookstore.Models;
using Bookstore.Services;
using Bookstore.Views;
using Bookstore.Views.TabbedPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;

namespace Bookstore.ViewModels.Startup
{
    public class LoginPageViewModel : BaseViewModel
    {
        public Command RequestLoginCommand { get; set; }        

        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        AccountApiService _apiServices = new AccountApiService();
        public LoginPageViewModel()
        {
            RequestLoginCommand = new Command(async () => await ExecuteRequestLoginCommand());
        }

        public async Task ExecuteRequestLoginCommand()
        {
            var errorCredentials = ValidCredentials();
            if (string.IsNullOrEmpty(errorCredentials))
            {
                var response = await _apiServices.LoginAsync(new LoginModel()
                {
                    Email = Email,
                    Password = Password
                });
                if (response)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new ParentTabbedPage());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Credentials are not valid", "Ok");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login", errorCredentials, "Ok");
            }
        }     

        private string ValidCredentials()
        {
            if(string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                return "Username or Password cannot be empty";
            }
            if(!Password.Equals(ConfirmPassword))
            {
                return "Passwords don't match";
            }
            return string.Empty;
        }       
    }
}
