using System;

namespace Auth0.NativeLogin.Facebook
{
    public interface IFacebookLoginService
    {
        string AccessToken { get; }
        string ProfileName { get; }
        Action<TokenResponse, TokenResponse> AccessTokenChanged { get; set; }
        Action<string, string> ProfileNameChanged { get; set; }
        void Logout();
    }
}
