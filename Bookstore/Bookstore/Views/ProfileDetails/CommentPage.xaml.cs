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
    public partial class CommentPage : ContentPage
    {
        public CommentPage()
        {
            InitializeComponent();
            this.BindingContext = new CommentPageViewModel();
        }

        private void DeleteComment_Tapped(object sender, EventArgs e)
        {
            string SysID = string.Empty;
            if (sender is Image)
            {
                SysID = (sender as Image).ClassId;
            }
            var viewModel = this.BindingContext as CommentPageViewModel;
            viewModel.RemoveComment(SysID);
        }

        private async void OpenBookDetail_Tapped(object sender, EventArgs e)
        {
            string SysID = string.Empty;
            if (sender is StackLayout)
            {
                SysID = (sender as StackLayout).ClassId;
            }
            var viewModel = this.BindingContext as CommentPageViewModel;
            await viewModel.ExecuteBookDetail(SysID);
        }
    }
}