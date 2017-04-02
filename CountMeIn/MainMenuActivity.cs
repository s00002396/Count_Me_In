using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System.Data.SqlClient;

namespace CountMeIn
{
    [Activity(Label = "Main Menu", MainLauncher = true)]
    public class MainMenuActivity : Activity
    {
        private Button btnUpComingevent;        
        private Button btnPendingInvite;
        private Button btnCreateGroup;
        private Button btnCreateEvent;

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
        }

        private void HandleEvents()
        {
            btnUpComingevent.Click += BtnUpComingevent_Click;            
            btnPendingInvite.Click += BtnPendingInvite_Click;
            btnCreateGroup.Click += BtnCreateGroup_Click;
            btnCreateEvent.Click += BtnCreateEvent_Click;           
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

        //private void BtnCreateDB_Click(object sender, EventArgs e)
        //{
        //    SqlConnection sqlconn;

        //    string connsqlstring = string.Format("Server=tcp:dominicbrennan.database.windows.net,1433;Initial Catalog=FYP_Project;Persist Security Info=False;User ID=dominicbrennan;Password=Fld118yi;MultipleActiveResultSets=False;Trusted_Connection=false;Encrypt=false;Connection Timeout=30;");           
        //    sqlconn = new System.Data.SqlClient.SqlConnection(connsqlstring);           
        //    try
        //    {                
        //        sqlconn.Open();
        //        //txtSysLog.Text = "Success Finally";
        //    }
        //    catch (Exception ex)
        //    {
        //        //txtSysLog.Text = ex.ToString();
        //    }
        //    finally
        //    {
        //        sqlconn.Close();
        //    }            
        //}

        //private void BtnCreateEventButton_Click(object sender, EventArgs e)
        //{
        //    var intent = new Intent(this, typeof(CreateEventActivity));
        //    StartActivity(intent);
        //}
    }
}