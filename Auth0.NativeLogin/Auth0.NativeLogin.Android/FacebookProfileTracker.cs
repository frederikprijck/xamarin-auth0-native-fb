using Auth0.NativeLogin.Facebook;
using Xamarin.Facebook;

namespace Auth0.NativeLogin.Droid
{
    class FacebookProfileTracker : ProfileTracker
    {
        readonly IFacebookLoginService facebookLoginService;

        public FacebookProfileTracker(FacebookLoginService facebookLoginService)
        {
            this.facebookLoginService = facebookLoginService;
        }

        protected override void OnCurrentProfileChanged(Profile oldProfile, Profile currentProfile)
        {
            facebookLoginService.ProfileNameChanged?.Invoke(oldProfile?.Name, currentProfile?.Name);

        }
    }
}