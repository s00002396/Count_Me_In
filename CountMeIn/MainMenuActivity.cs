using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System.Data.SqlClient;

namespace CountMeIn
{
    [Activity(Label = "Main Menu", MainLauncher = false)]
    public class MainMenuActivity : Activity
    {
        private Button btnUpComingevent;        
        private Button btnPendingInvite;
        private Button btnCreateGroup;
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
            btnCreateGroup = FindViewById<Button>(Resource.Id.createGroupButton);
            btnCreateEvent = FindViewById<Button>(Resource.Id.createEventButton);
            btnlogOut = FindViewById<Button>(Resource.Id.logOut);
        }
        private void HandleEvents()
        {
            btnUpComingevent.Click += BtnUpComingevent_Click;            
            btnPendingInvite.Click += BtnPendingInvite_Click;
            btnCreateGroup.Click += BtnCreateGroup_Click;
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
        private void BtnCreateGroup_Click(object sender, EventArgs e)
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