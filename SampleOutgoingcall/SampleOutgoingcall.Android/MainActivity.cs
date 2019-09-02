using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Telephony;
using Xamarin.Forms;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Permission = Plugin.Permissions.Abstractions.Permission;

namespace SampleOutgoingcall.Droid
{
    [Activity(Label = "SampleOutgoingcall", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }


        async void RequestPermissionAsync()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync<PhonePermission>();
            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Phone))
                {
                    // has no permission

                }

                status = await CrossPermissions.Current.RequestPermissionAsync<PhonePermission>();
            }

            if (status == PermissionStatus.Granted)
            {
                //Query permission
                StateListener phoneStateListener = new StateListener();
                TelephonyManager telephonyManager = (TelephonyManager)GetSystemService(TelephonyService);
                telephonyManager.Listen(phoneStateListener, PhoneStateListenerFlags.CallState);
            }

            else if (status != PermissionStatus.Unknown)
            {
                //permission denied
            }
        }




        public class StateListener : PhoneStateListener
        {
            public override void OnCallStateChanged(CallState state, string incomingNumber)
            {
                base.OnCallStateChanged(state, incomingNumber);
                switch (state)
                {
                    case CallState.Ringing:

                        try
                        {
                            MessagingCenter.Send<Object>(new Object(), "CallConnectedAndroid");
                        }
                        catch (Exception ex)
                        {
                        }
                        break;
                    case CallState.Offhook:

                        try
                        {
                            MessagingCenter.Send<Object>(new Object(), "CallEndedAndroid");
                        }
                        catch (Exception ex)
                        {
                        }

                        break;
                    case CallState.Idle:
                        break;
                }
            }
        }
    }




}