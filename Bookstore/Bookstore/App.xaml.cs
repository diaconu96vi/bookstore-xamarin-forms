using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Bookstore.Services;
using Bookstore.Views;
using Bookstore.SplashPages;

namespace Bookstore
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SplashPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
