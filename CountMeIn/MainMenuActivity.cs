using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System.Data.SqlClient;
using Android.Content.PM;

namespace CountMeIn
{
    [Activity(Label = "Main Menu", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainMenuActivity : Activity
    {
        private Button btnUpComingevent;        
        private Button btnPendingInvite;
        private Button btnCreateVenue;
        private Button btnCreateEvent;
        private Button btnlogOut;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);            
            SetContentView(Resource.Layout.MainMenu);
            FindViews();
            HandleEvents();
        }
        private void FindViews()
        {
            btnUpComingevent = FindViewById<Button>(Resource.Id.upcomingeventButton);
            btnPendingInvite = FindViewById<Button>(Resource.Id.pendingInviteButton);
            btnCreateVenue = FindViewById<Button>(Resource.Id.createVenueButton);
            btnCreateEvent = FindViewById<Button>(Resource.Id.createEventButton);
            btnlogOut = FindViewById<Button>(Resource.Id.logOut);
        }
        private void HandleEvents()
        {
            btnUpComingevent.Click += BtnUpComingevent_Click;            
            btnPendingInvite.Click += BtnPendingInvite_Click;
            btnCreateVenue.Click += BtnCreateVenue_Click;
            btnCreateEvent.Click += BtnCreateEvent_Click;
            btnlogOut.Click += BtnlogOut_Click;          
        }
        private void BtnlogOut_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(LoginActivity));
            StartActivity(intent);
        }
        private void BtnCreateEvent_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CreateEventActivity));
            StartActivity(intent);
        }
        private void BtnUpComingevent_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(UpComingEventActivity));
            StartActivity(intent);
        }
        private void BtnCreateVenue_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CreateGroupActivity));
            StartActivity(intent);
        }
        private void BtnPendingInvite_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(PendingEventActivity));
            StartActivity(intent);
        }
    }
}