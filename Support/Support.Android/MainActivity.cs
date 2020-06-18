using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;

namespace Support.Droid
{
    [Activity(Label = "Support", Icon = "@drawable/logo", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static Activity AppActivity;
        protected override void OnCreate(Bundle bundle)
        {
            try
            {                
                TabLayoutResource = Resource.Layout.Tabbar;
                
                ToolbarResource = Resource.Layout.Toolbar;
                
                AppActivity = this;
                base.OnCreate(bundle);                
                global::Xamarin.Forms.Forms.Init(this, bundle);
                Window.SetSoftInputMode(SoftInput.AdjustResize);
                UIHelper.assistActivity(this);

                LoadApplication(new App());

            }
            catch(Exception ex)
            {
                Log.Info("Ifeoluwa", ex.StackTrace);
                Log.Warn("Ifeoluwa", ex.Message);
            }
        }
    }
}

