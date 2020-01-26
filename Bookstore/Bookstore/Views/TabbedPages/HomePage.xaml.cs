using Bookstore.ViewModels.TabbedPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace Bookstore.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            this.BindingContext = new HomePageViewModel();
        }

        private async void BookDetail_Tapped(object sender, EventArgs e)
        {
            var titleLabel = (sender as Layout<View>).Children[1] as Label;

            HomePageViewModel viewModel = this.BindingContext as HomePageViewModel;
            await viewModel.ExecuteBookDetail(titleLabel.Text);
        }

        private async void GenreDetail_Tapped(object sender, EventArgs e)
        {
            var titleLabel = (sender as Layout<View>).Children[1] as Label;

            HomePageViewModel viewModel = this.BindingContext as HomePageViewModel;
            await viewModel.ExecuteGenreDetail(titleLabel.Text);
        }
    }
}