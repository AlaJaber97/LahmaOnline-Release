using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Views;
//using Plugin.LocalNotification;
using Android.Gms.Common;

namespace LahmaOnline.Droid
{

    [Activity(Label = "LahmaOnline", Icon = "@drawable/logoICON", Theme = "@style/SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,ScreenOrientation = ScreenOrientation.UserPortrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static Activity Instance { get; internal set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            Instance = this;
            base.Window.RequestFeature(WindowFeatures.ActionBar);
            base.SetTheme(Resource.Style.MainTheme);
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.SetFlags("FastRenderers_Experimental", "Shell_Experimental", "Visual_Experimental", "CollectionView_Experimental");
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            Plugin.InputKit.Platforms.Droid.Config.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App());
            //NotificationCenter.NotifyNotificationTapped(Intent);
        }

        protected override void OnNewIntent(Android.Content.Intent intent)
        {
            //NotificationCenter.NotifyNotificationTapped(intent);
            base.OnNewIntent(intent);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}