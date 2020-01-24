using Bookstore.Converters;
using Bookstore.Properties;
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

        }        
        public async Task ExecuteSignUpCommand()
        {

        }
    }
}
