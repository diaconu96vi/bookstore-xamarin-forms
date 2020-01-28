using Bookstore.CustomControls;
using Bookstore.Models;
using Bookstore.ViewModels.DetailPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bookstore.Views.DetailPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookDetailPage : ContentPage
    {
        public BookDetailPage(BookView item)
        {
            this.BindingContext = new BookDetailPageViewModel(item);
            InitializeComponent();
        } 

        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void MoreCommentsClick(object sender, EventArgs e)
        {
            //Navigation.PushModalAsync(new CommentsPage());
        }

        private void TapFavoriteIcon(object sender, EventArgs e)
        {
            BookDetailPageViewModel viewModel = this.BindingContext as BookDetailPageViewModel;
            viewModel.ConfigureFavoriteIcon();
        }

        private void TapRatingStars(object sender, EventArgs e)
        {
            Image imageSender;
            if (sender is Image)
            {
                imageSender = sender as Image;
            }
            else
            {
                return;
            }           
            BookDetailPageViewModel viewModel = this.BindingContext as BookDetailPageViewModel;
            viewModel.UpdateUserRating(imageSender.ClassId);
        }
    }
}