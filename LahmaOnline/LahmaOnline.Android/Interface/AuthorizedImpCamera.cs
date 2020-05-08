
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using LahmaOnline.Interface;
using Xamarin.Forms;
[assembly: Dependency(typeof(LahmaOnline.Droid.Interface.AuthorizedImpCamera))]
namespace LahmaOnline.Droid.Interface
{
    public class AuthorizedImpCamera : IAuthorizeCamera
    {
        public void Authorized()
        {
            var permission1 = Manifest.Permission.AccessCoarseLocation;
            var permission2 = Manifest.Permission.AccessFineLocation;
            var permission3 = Manifest.Permission.AccessLocationExtraCommands;
            var permission4 = Manifest.Permission.AccessMockLocation;
            var permission5 = Manifest.Permission.Camera;
            string[] camerapermission = { permission1, permission2, permission3, permission4 };
            var permission = hasPermissions((Activity)MainActivity.Instance, camerapermission);
            if (permission == false)
            {
                AuthorizeCameraUse();
            }
            MessagingCenter.Send<string>(permission.ToString(), "ResultAuthorization");
        }
        void AuthorizeCameraUse()
        {
            var permission1 = Manifest.Permission.AccessCoarseLocation;
            var permission2= Manifest.Permission.AccessFineLocation;
            var permission3 = Manifest.Permission.AccessLocationExtraCommands;
            var permission4 = Manifest.Permission.AccessMockLocation;
            var permission5 = Manifest.Permission.Camera;
            string[] camerapermission = { permission1, permission2, permission3, permission4 };
            if (ContextCompat.CheckSelfPermission((Activity)MainActivity.Instance, permission1) != (int)Permission.Granted
                            || ContextCompat.CheckSelfPermission((Activity)MainActivity.Instance, permission2) != (int)Permission.Granted
                            || ContextCompat.CheckSelfPermission((Activity)MainActivity.Instance, permission3) != (int)Permission.Granted
                            || ContextCompat.CheckSelfPermission((Activity)MainActivity.Instance, permission4) != (int)Permission.Granted)
            {
                ((Activity)MainActivity.Instance).RequestPermissions(camerapermission, 1);
                return;
            }
        }
        public static bool hasPermissions(Context context, string[] permissions)
        {
            if (context != null && permissions != null)
            {
                foreach (var item in permissions)
                {
                    if (ContextCompat.CheckSelfPermission(context, item) != (int)Permission.Granted)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}