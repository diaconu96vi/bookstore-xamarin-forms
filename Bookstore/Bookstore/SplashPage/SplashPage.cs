using System;
using System.Collections.Generic;
using System.Text;
using Bookstore.Converters;
using Plugin.SharedTransitions;
using Xamarin.Forms;

namespace Bookstore.SplashPages
{
    public class SplashPage : ContentPage
    {
        private Image _splashImage;

        public SplashPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var sub = new AbsoluteLayout();
            var bitmap = Properties.Resources.bookstorelogo;
            _splashImage = new Image
            {
                Source = BitmapConverter.ByteToImageSource(bitmap),
                WidthRequest = 200,
                HeightRequest = 200
            };
            AbsoluteLayout.SetLayoutFlags(_splashImage,
                AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(_splashImage,
                new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(_splashImage);
            //this.BackgroundColor = Color.FromHex("#429de3");
            this.Content = sub;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _splashImage.ScaleTo(1, 2000);
            await _splashImage.ScaleTo(0.9, 1500, Easing.Linear);
            await _splashImage.ScaleTo(150, 1200, Easing.Linear);
            Application.Current.MainPage = new SharedTransitionNavigationPage(new StartupPage());
        }
    }
}
