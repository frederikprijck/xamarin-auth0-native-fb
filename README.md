# Facebook Native Authentication using Xamarin with Auth0

This example demonstrates how you can integrate Native Facebook authentication in a Xamarin Forms application when using Auth0.

In general, the flow is as follows:

- Authenticate using the Facebook SDK
- Exchange the FB Access Token for a FB Session Token
- Exchange the FB Session Token for Auth0 Token(s)

## Dependencies

This example does not use any Auth0 specific SDK as most of the code is specific to the Xamarin Facebook SDK for [Android](https://www.nuget.org/packages/Xamarin.Facebook.Android/) and [iOS](https://www.nuget.org/packages/Xamarin.Facebook.iOS). 
The integration with Auth0 is limited to a single call to `oauth/token` (as mentioned in [our documentation](https://auth0.com/docs/authenticate/identity-providers/social-identity-providers/facebook-native)), which you can see in `Auth0Client.cs`, in the `Auth0.NativeLogin.Auth0` project.

## Configuration

### Facebook configuration

Create an application at https://developers.facebook.com/ and enable Facebook Login.

### Auth0 configuration

Enable the Facebook Social Connection in your Auth0 dashboard and enable it for the Auth0 client you want to use as explained in [our documentation](https://auth0.com/docs/authenticate/identity-providers/social-identity-providers/facebook-native).

Open `app.xaml.cs` (inside `Auth0.NativeLogin/Auth0.NativeLogin`) and replace the `Domain` and `ClientId` values with the appropriate values.

### Android specific configuration

In order to run this project on an Android emulator (or device), you need to rename `strings.xml/example` (inside `Auth0.NativeLogin/Auth0.NativeLogin.Android/Resources/values`) to `strings.xml` and fill in the Facebook App Id.

> It appears to be the case that you need to target `Android <=30` as the Xamarin Facebook SDK has a known issue that's fixed but not released: https://github.com/xamarin/FacebookComponents/issues/236

### iOS specific configuration

[TODO]