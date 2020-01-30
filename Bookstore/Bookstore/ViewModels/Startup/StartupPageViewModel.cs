using Bookstore.ApplicationUtils;
using Bookstore.Converters;
using Bookstore.Models;
using Bookstore.Properties;
using Bookstore.Services;
using Bookstore.Views.Startup;
using Bookstore.Views.TabbedPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;

namespace Bookstore.ViewModels
{
    public class StartupPageViewModel : BaseViewModel
    {
        public Command LoginCommand { get; set; }
        public Command FacebookLogin { get; set; }
        public Command SignUpCommand { get; set; }

        AccountApiService _apiServices = new AccountApiService();

        public StartupPageViewModel()
        {
            LoginCommand = new Command(async () => await ExecuteLoginCommand());
            SignUpCommand = new Command(async () => await ExecuteSignUpCommand());
            FacebookLogin = new Command(async () => await ExecuteFacebookLogin());
        }

        public async Task ExecuteLoginCommand()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }        
        public async Task ExecuteSignUpCommand()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SignupPage());
        }

        [Obsolete]
        public async Task ExecuteFacebookLogin()
        {
            string clientId = null;
            string redirectUri = null;
            if(Application.Current.Properties.TryGetValue(nameof(FacebookEmail), out object loggedInUser))
            {
                LoginModel loginModel = new LoginModel() { Email = (loggedInUser as FacebookEmail).Email };
                var appUser = await _apiServices.GetUser(loginModel);
                if(appUser == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Warning", "Something went wrong", "Cancel");
                    return;
                }
                ApplicationGeneralSettings.CurrentUser = appUser;
                ApplicationGeneralSettings.FacebookUser = loggedInUser as FacebookEmail;
                await Application.Current.MainPage.Navigation.PushAsync(new ParentTabbedPage());
                return;
            }


            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = AuthenticationConstants.FacebookiOSClientId;
                    redirectUri = AuthenticationConstants.FacebookiOSRedirectUrl;
                    break;

                case Device.Android:
                    clientId = AuthenticationConstants.FacebookAndroidClientId;
                    redirectUri = AuthenticationConstants.FacebookAndroidRedirectUrl;
                    break;
            }

            var authenticator = new OAuth2Authenticator(
                clientId,
                AuthenticationConstants.FacebookScope,
                new Uri(AuthenticationConstants.FacebookAuthorizeUrl),
                new Uri(AuthenticationConstants.FacebookAccessTokenUrl),
                null);

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
        }

        [Obsolete]
        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }


            if (e.IsAuthenticated)
            {
                if (authenticator.AuthorizeUrl.Host == "www.facebook.com")
                {
                    FacebookEmail facebookEmail = null;

                    var httpClient = new HttpClient();

                    var json = await httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=id,name,first_name,last_name,email,picture.type(large)&access_token=" + e.Account.Properties["access_token"]);

                    facebookEmail = JsonConvert.DeserializeObject<FacebookEmail>(json);
                    ApplicationGeneralSettings.FacebookUser = facebookEmail;
                    Application.Current.Properties.Add(nameof(FacebookEmail), facebookEmail);
                    ConfigureFacebookLogic(facebookEmail);
                }
            }
        }

        [Obsolete]
        void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }
        }

        private async void ConfigureFacebookLogic(FacebookEmail facebookEmail)
        {
            LoginModel loginModel = new LoginModel() { Email = (facebookEmail as FacebookEmail).Email };
            var result = await _apiServices.GetUser(loginModel);
            if (result != null)
            {
                ApplicationGeneralSettings.CurrentUser = result;
                await Application.Current.MainPage.Navigation.PushAsync(new ParentTabbedPage());
            }
            else
            {
                await ExecuteRegisterCommand(facebookEmail);
            }
        }

        public async Task ExecuteRegisterCommand(FacebookEmail facebookEmail)
        {
            var model = new SignupModel()
            {
                FullName = facebookEmail.Name,
                UserName = facebookEmail.Id,
                Email = facebookEmail.Email,
                Password = ApplicationGeneralSettings.Token,
                ConfirmPassword = ApplicationGeneralSettings.Token,
                IsAdmin = false
            };
            var isSuccess = await _apiServices.RegisterAsync(model);
            if (isSuccess.Item1)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new ParentTabbedPage());
            }
            else
            {
                if (isSuccess.Item2 != null && isSuccess.Item2.Any())
                {
                    var errorsString = IdentityErrorsConverter.IdentityErrorsToString(isSuccess.Item2);
                    await Application.Current.MainPage.DisplayAlert("Warning", errorsString, "Cancel");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Warning", "Something went wrong", "Cancel");
                }
            }
        }
    }
}
