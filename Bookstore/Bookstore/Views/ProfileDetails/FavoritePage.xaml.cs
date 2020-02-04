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
    public partial class FavoritePage : ContentPage
    {
        public FavoritePage()
        {
            InitializeComponent();
            this.BindingContext = new FavoritePageViewModel();
        }

        private void RemoveFavorite_Tapped(object sender, EventArgs e)
        {
            string SysID = string.Empty;
            if (sender is Image)
            {
                SysID = (sender as Image).ClassId;
            }
            var viewModel = this.BindingContext as FavoritePageViewModel;
            viewModel.RemoveFavorite(SysID);
        }

        private async void OpenBookDetail_Tapped(object sender, EventArgs e)
        {
            string SysID = string.Empty;
            if (sender is StackLayout)
            {
                SysID = (sender as StackLayout).ClassId;
            }
            var viewModel = this.BindingContext as FavoritePageViewModel;
            await viewModel.ExecuteBookDetail(SysID);
        }
    }
}