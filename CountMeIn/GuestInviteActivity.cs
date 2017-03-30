using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CountMeIn
{
    [Activity(Label = "Invite Guests", MainLauncher = false)]
    public class GuestInviteActivity : Activity
    {
        private Button btnInvite;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.GuestInvite);
            FindViews();

            HandleEvents();
        }
        private void FindViews()
        {
            btnInvite = FindViewById<Button>(Resource.Id.createInvite);          
            
        }

        private void HandleEvents()
        {
            btnInvite.Click += BtnInvite_Click;
        }

        private void BtnInvite_Click(object sender, EventArgs e)
        {
            Notification.Builder builder = new Notification.Builder(this)
            .SetContentTitle("Count-Me-In")
            .SetContentText("You have a new invite")
            .SetSmallIcon(Resource.Drawable.Icon);

            Notification notification = builder.Build();
            NotificationManager notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;

            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);

            //Toast.MakeText(this, "Invite Sent ", ToastLength.Long).Show();
            //var intent = new Intent(this, typeof(MainMenuActivity));
            //StartActivity(intent);
        }
    }
}