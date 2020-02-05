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
    public partial class OrderDetailPage : ContentPage
    {
        public OrderDetailPage(int OrderSysID)
        {
            InitializeComponent();
            this.BindingContext = new OrderDetailPageViewModel(OrderSysID);
        }

        private async void OpenBookDetail_Tapped(object sender, EventArgs e)
        {
            string SysID = string.Empty;
            if (sender is StackLayout)
            {
                SysID = (sender as StackLayout).ClassId;
            }
            var viewModel = this.BindingContext as OrderDetailPageViewModel;
            await viewModel.ExecuteBookDetail(SysID);
        }
    }
}