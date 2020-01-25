using Bookstore.ViewModels.TabbedPages;
using Bookstore.Views.Admin;
using Plugin.SharedTransitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bookstore.Views.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private bool open;
        public ProfilePage()
        {
            InitializeComponent();
            this.BindingContext = new ProfilePageViewModel();
        }

        private void OrderInfoClick(object sender, EventArgs e)
        {
            if (!(sender is StackLayout stack)) return;
            switch (stack.ClassId)
            {
                case "MyOder":
                    //OpenPage(new MyOrderPage());
                    break;

                case "MyFav":
                    //OpenPage(new MyFavoritePage());
                    break;

                case "LastView":
                    //OpenPage(new LastViewPage());
                    break;

                case "MyComments":
                    //OpenPage(new MyCommentsPage());
                    break;
                case "AddBooks":
                    OpenPage(new AddBooksPage());
                    break;
            }
        }

        private async void OpenPage(Page page)
        {
            await Navigation.PushAsync(page);
        }

        private void ChancePassClick(object sender, EventArgs e)
        {
            //Navigation.PushModalAsync(new ChangePasswordPage(), true);
        }

        private void ContactClick(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("mailto:diaconu.ionut96@gmail.com"));
        }

        private void LogOutClick(object sender, EventArgs e)
        {
            Application.Current.MainPage = new SharedTransitionNavigationPage(new MainPage());
        }

        private void SettingClick(object sender, EventArgs e)
        {
            if(open)
            {
                open = false;
            }
            else
            {
                open = true;
            }

            SettingsView.IsVisible = open;
        }
    }
}