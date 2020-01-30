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
                var AppUser = new AppUser()
                {
                    UserName = (loggedInUser as FacebookEmail).Name,
                    Email = (loggedInUser as FacebookEmail).Email,
                    IsAdmin = false
                };
                ApplicationGeneralSettings.CurrentUser = AppUser;
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
                    ConfigureFacebookLogic(facebookEmail);
                }
                else
                {
                    GmailUser user = null;

                    // If the user is authenticated, request their basic user data from Google
                    // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
                    var request = new OAuth2Request("GET", new Uri(AuthenticationConstants.GoogleUserInfoUrl), null, e.Account);
                    var response = await request.GetResponseAsync();
                    if (response != null)
                    {
                        // Deserialize the data and store it in the account store
                        // The users email address will be used to identify data in SimpleDB
                        string userJson = await response.GetResponseTextAsync();
                        user = JsonConvert.DeserializeObject<GmailUser>(userJson);
                    }

                    Application.Current.Properties.Remove("Id");
                    Application.Current.Properties.Remove("FirstName");
                    Application.Current.Properties.Remove("LastName");
                    Application.Current.Properties.Remove("DisplayName");
                    Application.Current.Properties.Remove("EmailAddress");
                    Application.Current.Properties.Remove("ProfilePicture");

                    Application.Current.Properties.Add("Id", user.Id);
                    Application.Current.Properties.Add("DisplayName", user.Name);
                    Application.Current.Properties.Add("EmailAddress", user.Email);
                    Application.Current.Properties.Add("ProfilePicture", user.Picture);
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
            var result = await _apiServices.GetUser(new LoginModel() { Email = facebookEmail.Email });
            if(result)
            {
                var AppUser = new AppUser()
                {
                    //Trebuie un get cu useru pt sysid
                    UserName = facebookEmail.Name,
                    Email = facebookEmail.Email,
                    IsAdmin = false
                };
                //Application.Current.Properties.Add(nameof(FacebookEmail), facebookEmail);
                ApplicationGeneralSettings.CurrentUser = AppUser;
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
                UserName = facebookEmail.Name,
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
