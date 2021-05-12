using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


namespace eLiDAR.Droid
{
    [Activity(Label = "eVSN", Icon = "@mipmap/Heatmap", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.FullSensor)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            Xamarin.Essentials.Platform.Init(this, bundle); // add this line to your code, it may also be called: bundle

            global::Xamarin.Forms.Forms.SetFlags("Shell_Experimental", "CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init(this, bundle);
            // printHashKey 
           
            LoadApplication(new App());
        }

    }
}

