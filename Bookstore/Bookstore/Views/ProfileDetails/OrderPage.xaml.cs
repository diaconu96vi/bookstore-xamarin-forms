using Bookstore.ViewModels.ProfileDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bookstore.Views.ProfileDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPage : ContentPage
    {
        public OrderPage()
        {
            InitializeComponent();
            this.BindingContext = new OrderPageViewModel();
        }

        private async void OpenOrderDetails_Tapped(object sender, EventArgs e)
        {
            string SysID = string.Empty;
            if (sender is StackLayout)
            {
                SysID = (sender as StackLayout).ClassId;
            }
            var viewModel = this.BindingContext as OrderPageViewModel;
            await viewModel.OpenOrderDetail(SysID);
        }
    }
}