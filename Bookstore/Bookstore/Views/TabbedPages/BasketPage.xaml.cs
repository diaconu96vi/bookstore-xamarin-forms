using Bookstore.ViewModels.TabbedPages;
using Bookstore.Views.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bookstore.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasketPage : ContentPage
    {
        public BasketPage()
        {
            InitializeComponent();
            this.BindingContext = new BasketPageViewModel();
        }

        private void Button_OnPressed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddAddressClick(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddressPage());
        }

        private void ContinueClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreditCardPage(), true);
        }
    }
}