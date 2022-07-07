using Auth0.NativeLogin.Facebook;
using Xamarin.Forms;

namespace Auth0.NativeLogin.Controls
{
    public class FacebookLoginButton : View
    {
        public string[] Permissions
        {
            get { return (string[])GetValue(PermissionsProperty); }
            set { SetValue(PermissionsProperty, value); }
        }
        public static readonly BindableProperty PermissionsProperty =
            BindableProperty.Create(
                nameof(Permissions),
                typeof(string[]),
                typeof(FacebookLoginButton),
                defaultValue: new string[] { "public_profile", "email" });

        public Command<TokenResponse> OnSuccess
        {
            get { return (Command<TokenResponse>)GetValue(OnSuccessProperty); }
            set { SetValue(OnSuccessProperty, value); }
        }

        public static readonly BindableProperty OnSuccessProperty =
            BindableProperty.Create(nameof(OnSuccess), typeof(Command<TokenResponse>), typeof(FacebookLoginButton));

        public Command<string> OnError
        {
            get { return (Command<string>)GetValue(OnErrorProperty); }
            set { SetValue(OnErrorProperty, value); }
        }

        public static readonly BindableProperty OnErrorProperty =
            BindableProperty.Create(nameof(OnError), typeof(Command<string>), typeof(FacebookLoginButton));

        public Command OnCancel
        {
            get { return (Command)GetValue(OnCancelProperty); }
            set { SetValue(OnCancelProperty, value); }
        }

        public static readonly BindableProperty OnCancelProperty =
            BindableProperty.Create(nameof(OnCancel), typeof(Command), typeof(FacebookLoginButton));
    }
}
