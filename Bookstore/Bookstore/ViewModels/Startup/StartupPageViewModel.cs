using Bookstore.Converters;
using Bookstore.Properties;
using Bookstore.Views.Startup;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bookstore.ViewModels
{
    public class StartupPageViewModel : BaseViewModel
    {
        public Command LoginCommand { get; set; }

        public Command SignUpCommand { get; set; }

        public StartupPageViewModel()
        {
            LoginCommand = new Command(async () => await ExecuteLoginCommand());
            SignUpCommand = new Command(async () => await ExecuteSignUpCommand());        
        }

        public async Task ExecuteLoginCommand()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }        
        public async Task ExecuteSignUpCommand()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SignupPage());
        }
    }
}
