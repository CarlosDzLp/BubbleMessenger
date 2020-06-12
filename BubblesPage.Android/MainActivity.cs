using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.CurrentActivity;
using Android.Content;
using Android.Support.V4.App;
using Android;

namespace BubblesPage.Droid
{
    [Activity(Label = "BubblesPage", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private static int CODE_DRAW_OVER_OTHER_APP_PERMISSION = 2084;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Permissions();

            if (Build.VERSION.SdkInt >= BuildVersionCodes.M && !Android.Provider.Settings.CanDrawOverlays(this))
            {
                var intent = new Intent(Android.Provider.Settings.ActionManageOverlayPermission,Android.Net.Uri.Parse("package:com.companyname.bubblespage"));
                StartActivityForResult(intent, CODE_DRAW_OVER_OTHER_APP_PERMISSION);



                //Intent intent = new Intent(Android.Provider.Settings.ActionManageOverlayPermission, Android.Net.Uri.Parse("package" + PackageName));
                //StartActivityForResult(intent, CODE_DRAW_OVER_OTHER_APP_PERMISSION);
                //startActivityForResult(intent, CODE_DRAW_OVER_OTHER_APP_PERMISSION);
            }
            LoadApplication(new App());
        }
        private void Permissions()
        {
            ActivityCompat.RequestPermissions(this, new string[]
                {
                    Manifest.Permission.SystemAlertWindow
                }, 0);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == CODE_DRAW_OVER_OTHER_APP_PERMISSION)
            {

                if (Android.Provider.Settings.CanDrawOverlays(this))
                {
                    
                }
                else
                {
                    
                }
            }
            base.OnActivityResult(requestCode, resultCode, data);
        }
    

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}