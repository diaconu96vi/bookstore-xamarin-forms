using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using PayPal.Forms.Abstractions;
using PayPal.Forms;
using Android.Content;

namespace Bookstore.Droid
{
    [Activity(Label = "Bookstore", Icon = "@drawable/bookstorelogo", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            var config = new PayPalConfiguration(PayPalEnvironment.NoNetwork, "ARIHKnPqN07GY-CuoPFnZ6C82IXR9EpK5wFRQDaUv8qIfgFZZUyRM7CbpVL2xLYo3hhZPS2bpEukyYpo")
            {
                //If you want to accept credit cards
                AcceptCreditCards = false,
                //Your business name
                MerchantName = "Test Store",
                //Your privacy policy Url
                MerchantPrivacyPolicyUri = "https://www.example.com/privacy",
                //Your user agreement Url
                MerchantUserAgreementUri = "https://www.example.com/legal",
            };
            CrossPayPalManager.Init(config, this);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            PayPalManagerImplementation.Manager.OnActivityResult(requestCode, resultCode, data);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            PayPalManagerImplementation.Manager.Destroy();
        }
    }
}