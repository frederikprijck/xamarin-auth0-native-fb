using Auth0.NativeLogin.Facebook;
using System;
using Xamarin.Facebook.Login;

namespace Auth0.NativeLogin.Droid
{
    public class FacebookLoginService : IFacebookLoginService
    {
        private readonly FacebookAccessTokenTracker myAccessTokenTracker;
        private readonly FacebookProfileTracker myProfileTracker;
        public Action<string, string> ProfileNameChanged { get; set; }
        public Action<TokenResponse, TokenResponse> AccessTokenChanged { get; set; }

        public FacebookLoginService()
        {
            myAccessTokenTracker = new FacebookAccessTokenTracker(this);
            myProfileTracker = new FacebookProfileTracker(this);
            myAccessTokenTracker.StartTracking();
            myProfileTracker.StartTracking();
        }

        public string AccessToken => Xamarin.Facebook.AccessToken.CurrentAccessToken?.Token;
        public string ProfileName => Xamarin.Facebook.Profile.CurrentProfile?.Name;

        public void Logout()
        {
            LoginManager.Instance.LogOut();
        }
    }
}