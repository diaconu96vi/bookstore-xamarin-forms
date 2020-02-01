using Bookstore.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bookstore.Views.Order
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreditCardPage : ContentPage
    {
        public CreditCardPage()
        {
            InitializeComponent();
			this.BindingContext = new CreditCardPageViewModel();
        }

		private void AddCardClick(object sender, EventArgs e)
		{
			Navigation.PushModalAsync(new AddNewCardPage(), true);
		}

		private void BackButton(object sender, EventArgs e)
		{
			Navigation.PopAsync(true);
		}

		private async void ContinueOrderButton(object sender, EventArgs e)
		{
			//await Navigation.PushAsync(new SuccessPage());
		}
	}
}