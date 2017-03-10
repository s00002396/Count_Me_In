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

        private Button btnCreateDB;
        private TextView txtSysLog;

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

            btnCreateDB = FindViewById<Button>(Resource.Id.aboutButton);
            txtSysLog = FindViewById<TextView>(Resource.Id.txtSysLog);
        }

        private void HandleEvents()
        {
            btnUpComingevent.Click += BtnUpComingevent_Click;
            btnCreateEvent.Click += BtnCreateEventButton_Click;
            btnPendingInvite.Click += BtnPendingInvite_Click;
            btnCreateGroup.Click += BtnCreateGroup_Click;

            btnCreateDB.Click += BtnCreateDB_Click;
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
            var intent = new Intent(this, typeof(PendingInvitesActivity));
            StartActivity(intent);
        }

        private void BtnCreateDB_Click(object sender, EventArgs e)
        {
            SqlConnection sqlconn;

            string connsqlstring = string.Format("Server=tcp:dominicbrennan.database.windows.net,1433;Initial Catalog=FYP_Project;Persist Security Info=False;User ID=dominicbrennan;Password=Fld118yi;MultipleActiveResultSets=False;Trusted_Connection=false;Encrypt=false;Connection Timeout=30;");           
            sqlconn = new System.Data.SqlClient.SqlConnection(connsqlstring);           
            try
            {                
                sqlconn.Open();
                txtSysLog.Text = "Success Finally";
            }
            catch (Exception ex)
            {
                txtSysLog.Text = ex.ToString();
            }
            finally
            {
                sqlconn.Close();
            }
            //DBRepository dbr = new DBRepository();
            //var result = dbr.CreateDB();
            //Toast.MakeText(this, result, ToastLength.Short).Show();
        }

        private void BtnCreateEventButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CreateEventActivity));
            StartActivity(intent);
        }
    }
}