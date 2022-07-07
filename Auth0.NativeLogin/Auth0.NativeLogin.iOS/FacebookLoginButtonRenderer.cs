using Auth0.NativeLogin.Controls;
using Auth0.NativeLogin.Facebook;
using Auth0.NativeLogin.iOS;
using Facebook.LoginKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(FacebookLoginButton), typeof(FacebookLoginButtonRenderer))]
namespace Auth0.NativeLogin.iOS
{
    public class FacebookLoginButtonRenderer : ViewRenderer
    {
        bool disposed;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                var fbLoginBtnView = e.NewElement as FacebookLoginButton;
                var fbLoginbBtnCtrl = new LoginButton
                {
                    Permissions = fbLoginBtnView.Permissions
                };

                fbLoginbBtnCtrl.Completed += AuthCompleted;

                SetNativeControl(fbLoginbBtnCtrl);
            }
        }

        void AuthCompleted(object sender, LoginButtonCompletedEventArgs args)
        {
            var view = (Element as FacebookLoginButton);
            if (args.Error != null)
            {
                // Handle if there was an error
                view.OnError?.Execute(args.Error.ToString());

            }
            else if (args.Result.IsCancelled)
            {
                // Handle if the user cancelled the login request
                view.OnCancel?.Execute(null);
            }
            else
            {
                // Handle your successful login
                view.OnSuccess?.Execute(new TokenResponse { AccessToken = args.Result.Token.TokenString, ApplicationId = args.Result.Token.AppId });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !disposed)
            {
                if (Control != null)
                {
                    (Control as LoginButton).Completed -= AuthCompleted;
                    Control.Dispose();
                }
                disposed = true;
            }
            base.Dispose(disposing);
        }
    }
}
