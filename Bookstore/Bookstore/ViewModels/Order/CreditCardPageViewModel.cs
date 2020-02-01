using Bookstore.Views.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bookstore.ViewModels.Order
{
    public class CreditCardPageViewModel : BaseViewModel
    {
        public Command ConfirmOrderCommand { get; set; }
        public CreditCardPageViewModel()
        {
            ConfirmOrderCommand = new Command(async () => await ExecuteConfirmOrderCommand());
        }

        public async Task ExecuteConfirmOrderCommand()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SuccessOrderPage());
        }
    }
}
