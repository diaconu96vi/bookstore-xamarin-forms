﻿using Bookstore.Converters;
using Bookstore.Models;
using Bookstore.Services;
using Bookstore.Views;
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
                
                await Application.Current.MainPage.Navigation.PushAsync(new ItemsPage());
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
        
        public async Task ExecuteFacebookLogin()
        {
            
        }
    }
}
