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
using System.Data.SqlClient;
using System.Data;

namespace CountMeIn
{
    [Activity(Label = "Pending Invites")]
    public class PendingInvitesActivity : Activity
    {
        SqlConnection sqlconn;
        string eventDate;
        string eventName;
        string eventID;
        private TextView txtEventDate;
        private TextView txtEventName;
        private Button sendInvite;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            #region sql Connection
            string connsqlstring = string.Format("Server=tcp:dominicbrennan.database.windows.net,1433;Initial Catalog=CountMeIn;Persist Security Info=False;User ID=dominicbrennan;Password=Fld118yi;MultipleActiveResultSets=False;Trusted_Connection=false;Encrypt=false;Connection Timeout=30;");
            sqlconn = new System.Data.SqlClient.SqlConnection(connsqlstring);
            #endregion
            SetContentView(Resource.Layout.PendingInvites);
            eventID = Intent.GetStringExtra("Event_Id") ?? "Data not available";
            eventDate = Intent.GetStringExtra("Event Date") ?? "Data not available";
            eventName = Intent.GetStringExtra("Event Venue") ?? "Data not available";
            //eventID = Intent.GetStringExtra("Event_Id") ?? "Data not available";
            //Toast.MakeText(this, "No Pending Invites ", ToastLength.Long).Show();
            FindViews();

            
            HandleEvents();

            txtEventDate.Text = eventDate;
            txtEventName.Text = eventName;
            var test = eventID;
            //update the Event_Member_Table Going field
        }

        private void FindViews()
        {
            txtEventDate = FindViewById<TextView>(Resource.Id.txtEventDate);
            txtEventName = FindViewById<TextView>(Resource.Id.txtEventName);
            sendInvite = FindViewById<Button>(Resource.Id.sendInvite);
        }

        private void HandleEvents()
        {
            sendInvite.Click += SendInvite_Click;
        }

        private void SendInvite_Click(object sender, EventArgs e)
        {
            try
            {
                sqlconn.Open();
                SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlconn;
            cmd.CommandText = "UPDATE Event_Member_Table SET Going=@Going where Event_Member_Table.Member_Id like 101 and Event_Member_Table.Event_Id like @E_ID ";
            cmd.Parameters.AddWithValue("@E_ID", eventID);//get this from eventID
            cmd.Parameters.AddWithValue("@Going", 1);
            cmd.ExecuteNonQuery();
            //sqlconn.Close();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Error" + ex, ToastLength.Long).Show();
            }
            finally
            {
                sqlconn.Close();
                Toast.MakeText(this, "Invite accepted ", ToastLength.Long).Show();
                var intent = new Intent(this, typeof(PendingEventActivity));
                StartActivity(intent);
            }
        }
    }
}