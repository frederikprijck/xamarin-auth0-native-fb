using Auth0.NativeLogin.Facebook;
using Xamarin.Facebook;

namespace Auth0.NativeLogin.Droid
{
    class FacebookAccessTokenTracker : AccessTokenTracker
    {
        readonly IFacebookLoginService facebookLoginService;

        public FacebookAccessTokenTracker(FacebookLoginService facebookLoginService)
        {
            this.facebookLoginService = facebookLoginService;
        }

        protected override void OnCurrentAccessTokenChanged(AccessToken oldAccessToken, AccessToken currentAccessToken)
        {
            facebookLoginService.AccessTokenChanged?.Invoke(new TokenResponse { AccessToken = oldAccessToken?.Token, ApplicationId = oldAccessToken?.ApplicationId }, new TokenResponse { AccessToken = currentAccessToken?.Token, ApplicationId = currentAccessToken?.ApplicationId });
        }
    }
}