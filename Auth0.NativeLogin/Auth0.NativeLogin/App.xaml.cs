using Auth0.NativeLogin.Facebook;
using Xamarin.Forms;

namespace Auth0.NativeLogin
{
    public partial class App : Application
    {
        public IFacebookLoginService FacebookLoginService { get; private set; }
        public string Domain { get; set; } = "";
        public string ClientId { get; set; } = "";

        public App(IFacebookLoginService facebookLoginService)
        {
            InitializeComponent();

            FacebookLoginService = facebookLoginService;

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
