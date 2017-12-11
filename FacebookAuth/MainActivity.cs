using Android.App;
using Android.Widget;
using Android.OS;
using Android.Util;
using Android.Views;
using Java.Lang;
using Android.Webkit;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;

namespace FacebookAuth
{
    [Activity(Label = "FacebookAuth", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public WebView web_view;
        public TextView textView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            FacebookSdk.SdkInitialize(this.ApplicationContext);
            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            Button button = FindViewById<Button>(Resource.Id.loginButton);
            button.Click += (o, e) =>
            {
                LoginManager.Instance.LogOut();
                web_view = FindViewById<WebView>(Resource.Id.webview);
                web_view.Settings.JavaScriptEnabled = true;
                web_view.SetWebViewClient(new HelloWebViewClient(this));
                web_view.LoadUrl("https://www.facebook.com/v2.10/dialog/oauth?client_id=1123230621154670&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html");
            };
            web_view = FindViewById<WebView>(Resource.Id.webview);
            web_view.Settings.JavaScriptEnabled = true;
            web_view.SetWebViewClient(new HelloWebViewClient(this));
            web_view.LoadUrl("https://www.facebook.com/v2.10/dialog/oauth?client_id=1123230621154670&response_type=token&redirect_uri=https://www.facebook.com/connect/login_success.html");
            textView = FindViewById<TextView>(Resource.Id.textView);
        }
    }
    public class HelloWebViewClient : WebViewClient
    {
        private MainActivity main;
        public HelloWebViewClient(MainActivity main)
        {
            this.main = main; 
        }

        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            view.LoadUrl(url);
            if (view.Url.Contains("access_token") && view.Url.Contains("expires_in"))
            {
                string access_token = view.Url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");
                Toast toast = Toast.MakeText(main, access_token, ToastLength.Long);
                toast.SetGravity(GravityFlags.Center, 0, 0);
                toast.Show();
                
            }
            
            return false;
        }
    }

}

