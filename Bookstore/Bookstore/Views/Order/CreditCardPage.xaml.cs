using Bookstore.CustomControls;
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

		private void DeleteCard_Tapped(object sender, EventArgs e)
		{
			string SysID = string.Empty;
			if(sender is Image)
			{
				SysID = (sender as Image).ClassId;
			}
			CreditCardPageViewModel viewModel = this.BindingContext as CreditCardPageViewModel;
			viewModel.DeleteSelectedCard(SysID);
		}

		private void SelectCard_Tapped(object sender, EventArgs e)
		{
			string SysID = string.Empty;
			if (sender is IconView)
			{
				SysID = (sender as IconView).ClassId;
			}
			CreditCardPageViewModel viewModel = this.BindingContext as CreditCardPageViewModel;
			viewModel.SelectCardCheck(SysID);
		}
	}
}