using Auth0.NativeLogin.Auth0;
using Auth0.NativeLogin.Facebook;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Auth0.NativeLogin.ViewModels
{
    public class MainPageViewModel: BaseViewModel
    {
        readonly IFacebookLoginService facebookLoginService;

        private bool isAuthenticated;
        public bool IsAuthenticated
        {
            get { return this.isAuthenticated; }
            set { this.SetPropertyValue(ref this.isAuthenticated, value); }
        }

        private bool isNotAuthenticated;
        public bool IsNotAuthenticated
        {
            get { return this.isNotAuthenticated; }
            set { this.SetPropertyValue(ref this.isNotAuthenticated, value); }
        }

        private string profileName;
        public string ProfileName
        {
            get { return this.profileName; }
            set { this.SetPropertyValue(ref this.profileName, value); }
        }

        public ICommand OnFacebookLoginSuccessCmd { get; }
        public ICommand OnFacebookLoginErrorCmd { get; }
        public ICommand OnFacebookLoginCancelCmd { get; }
        public Command FacebookLogoutCmd { get; }

        public MainPageViewModel()
        {
            facebookLoginService = (Application.Current as App).FacebookLoginService;
            facebookLoginService.AccessTokenChanged = (Facebook.TokenResponse oldToken, Facebook.TokenResponse newToken) =>
            {
                FacebookLogoutCmd.ChangeCanExecute();
                IsAuthenticated = !string.IsNullOrEmpty(facebookLoginService.AccessToken);
                IsNotAuthenticated = !IsAuthenticated;
            };

            facebookLoginService.ProfileNameChanged = (string oldProfileName, string currentProfileName) =>
            {
                ProfileName = currentProfileName;
            };

            FacebookLogoutCmd = new Command(() =>
                facebookLoginService.Logout(),
                () => !string.IsNullOrEmpty(facebookLoginService.AccessToken));

            OnFacebookLoginSuccessCmd = new Command<Facebook.TokenResponse>(
                (response) =>
                {
                    var result = ExchangeFbTokenForAuth0(response.ApplicationId, response.AccessToken).Result;

                    DisplayAlert("Success", $"Exchange with Auth0 succeed: {result.AccessToken}");

                });

            OnFacebookLoginErrorCmd = new Command<string>(
                (err) => DisplayAlert("Error", $"Authentication failed: {err}"));

            OnFacebookLoginCancelCmd = new Command(
                () => DisplayAlert("Cancel", "Authentication cancelled by the user."));

            IsAuthenticated = !string.IsNullOrEmpty(facebookLoginService.AccessToken);
            IsNotAuthenticated = !IsAuthenticated;
            ProfileName = facebookLoginService.ProfileName;
        }

        void DisplayAlert(string title, string msg) =>
            (Application.Current as App).MainPage.DisplayAlert(title, msg, "OK");

        private async Task<Auth0.TokenResponse> ExchangeFbTokenForAuth0(string applicationId, string accessToken)
        {
            var domain = (Application.Current as App).Domain;
            var clientId = (Application.Current as App).ClientId;

            var fbClient = new FbClient();
            var auth0Client = new Auth0Client(domain, clientId);

            // 1. Exchange the access token for a session token
            var sessionToken = await fbClient.getSessionInfoAccessToken(applicationId, accessToken).ConfigureAwait(false);

            // 2. Exchange the session token for Auth0 token(s)
            var auth0Response = await auth0Client.ExchangeFbToken("Test", sessionToken).ConfigureAwait(false);

            return auth0Response;
        }
    }
}
