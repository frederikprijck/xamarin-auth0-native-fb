using Auth0.NativeLogin.Facebook;
using Facebook.LoginKit;
using FbCoreKit = Facebook.CoreKit;
using Foundation;
using System;

namespace Auth0.NativeLogin.iOS
{
    public class FacebookLoginService : IFacebookLoginService
    {
        public string AccessToken => FbCoreKit.AccessToken.CurrentAccessToken?.TokenString;
        public string ProfileName => FbCoreKit.Profile.CurrentProfile?.Name;

        public Action<TokenResponse, TokenResponse> AccessTokenChanged { get; set; }
        public Action<string, string> ProfileNameChanged { get; set; }

        public FacebookLoginService()
        {
            // TODO: Remove observer
            NSNotificationCenter.DefaultCenter.AddObserver(
                new NSString(FbCoreKit.AccessToken.DidChangeNotification),
                (n) =>
                {
                    var oldToken = n.UserInfo[FbCoreKit.AccessToken.OldTokenKey] as FbCoreKit.AccessToken;
                    var newToken = n.UserInfo[FbCoreKit.AccessToken.NewTokenKey] as FbCoreKit.AccessToken;
                    var oldTokenResponse = new TokenResponse
                    {
                        AccessToken = oldToken?.TokenString,
                        ApplicationId = oldToken?.AppId,
                    };
                    var newTokenResponse = new TokenResponse
                    {

                        AccessToken = newToken?.TokenString,
                        ApplicationId = newToken?.AppId,
                    };
                    AccessTokenChanged?.Invoke(oldTokenResponse, newTokenResponse);
                });
            NSNotificationCenter.DefaultCenter.AddObserver(
               new NSString(FbCoreKit.Profile.DidChangeNotification),
               (n) =>
               {
                   var oldProfile = n.UserInfo[FbCoreKit.Profile.OldProfileKey] as FbCoreKit.Profile;
                   var newProfile = n.UserInfo[FbCoreKit.Profile.NewProfileKey] as FbCoreKit.Profile;
                   ProfileNameChanged?.Invoke(
                       oldProfile?.Name,
                       newProfile?.Name
                    );
               });
        }

        public void Logout()
        {
            using (var loginManager = new LoginManager())
            {
                loginManager.LogOut();
            }
        }
    }
}
