using Bookstore.Renderers;
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

        private void GenreDetail_Tapped(object sender, EventArgs e)
        {
            var titleLabel = (sender as Layout<View>).Children[1] as Label;

            HomePageViewModel viewModel = this.BindingContext as HomePageViewModel;
            viewModel.ExecuteGenreDetail(titleLabel.Text);
        }
        
        private void AuthorsFilters_Tapped(object sender, EventArgs e)
        {
            string authorLabel = string.Empty;
            if(sender is Label)
            {
                authorLabel = (sender as Label).Text;
            }

            HomePageViewModel viewModel = this.BindingContext as HomePageViewModel;
            viewModel.ApplyAuthorFilter(authorLabel);
        }

        private void BorderlessSearchBar_Unfocused(object sender, FocusEventArgs e)
        {
            BorderlessSearchBar borderlessSearchBar;
            if(sender is BorderlessSearchBar)
            {
                borderlessSearchBar = sender as BorderlessSearchBar;
            }
            HomePageViewModel viewModel = this.BindingContext as HomePageViewModel;
            viewModel.FilterSearchBar();

        }
    }
}