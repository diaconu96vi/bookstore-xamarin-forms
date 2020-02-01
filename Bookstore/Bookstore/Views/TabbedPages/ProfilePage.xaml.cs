using Bookstore.ApplicationUtils;
using Bookstore.ViewModels.TabbedPages;
using Bookstore.Views.Admin;
using Bookstore.Views.ProfileDetails;
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
        private bool openAdmin;
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
                    OpenModalPage(new ManageBooksPage());
                    break;
                case "ManageBookGenres":
                    OpenModalPage(new ManageBookGenresPage());
                    break;
                case "ManageGenres":
                    OpenModalPage(new ManageGenresPage());
                    break;
                case "ManageAuthors":
                    OpenModalPage(new ManageAuthorsPage());
                    break;
                case "ManagePublishers":
                    OpenModalPage(new ManagePublishersPage());
                    break;
            }
        }

        private async void OpenPage(Page page)
        {
            await Navigation.PushAsync(page);
        }

        private async void OpenModalPage(Page page)
        {
            await Navigation.PushModalAsync(page, true);
        }

        private async void ChangePassClick(object sender, EventArgs e)
        {
            if (ApplicationGeneralSettings.FacebookUser == null)
                await Navigation.PushModalAsync(new ChangePasswordPage(), true);
            else
                await Application.Current.MainPage.DisplayAlert("Warning", "Cannot change password for facebook account!", "Cancel");
        }

        private void ChangeDetailsClick(object sender, EventArgs e)
        {
        }

        private void ContactClick(object sender, EventArgs e)
        {
            Launcher.OpenAsync(new Uri("mailto:diaconu.ionut96@gmail.com"));
        }

        private void LogOutClick(object sender, EventArgs e)
        {
            if(ApplicationGeneralSettings.FacebookUser != null)
            {
                ApplicationGeneralSettings.FacebookUser = null;
            }
            ShoppingBasket.Instance.Clear();
            ApplicationGeneralSettings.CurrentUser = null;
            Application.Current.MainPage = new SharedTransitionNavigationPage(new StartupPage());
        }

        private void SettingClick(object sender, EventArgs e)
        {
            if (open)
            {
                open = false;
            }
            else
            {
                open = true;
            }

            SettingsView.IsVisible = open;
        }

        private void AdminClick(object sender, EventArgs e)
        {
            if (openAdmin)
            {
                openAdmin = false;
            }
            else
            {
                openAdmin = true;
            }

            AdminView.IsVisible = openAdmin;
        }
    }
}