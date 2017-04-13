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
using CountMeIn.Model;
using CountMeIn.Service;
using System.Data.SqlClient;
using System.Data;
using ListViewEvents;
using CountMeIn.Adapters;

namespace CountMeIn
{
    [Activity(Label = "Pending Invites", MainLauncher = true)]
    public class PendingEventActivity : Activity
    {
        private ListView pendingEventListView;

        private List<Person> mItems;
        private ListView mListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PendingEventView);
            FindViews();

            /********Get the Mobile Number**********/
            Android.Telephony.TelephonyManager tMgr = (Android.Telephony.TelephonyManager)this.GetSystemService(Android.Content.Context.TelephonyService);
            string mPhoneNumber = tMgr.Line1Number;
            /****************************************/
            mItems = new List<Person>(); 
            HandleEvents();
            MyPendingEventListAdapter adapter = new MyPendingEventListAdapter(this, mItems);
            pendingEventListView.Adapter = adapter;
            pendingEventListView.ItemLongClick += EventListView_ItemLongClick;
            pendingEventListView.ItemClick += PendingEventListView_ItemClick;
        }

        private void PendingEventListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof(PendingInvitesActivity));
            intent.PutExtra("Event Date", mItems[e.Position].EventDate);
            intent.PutExtra("Event Venue", mItems[e.Position].EventName);
            intent.PutExtra("Event_Id", mItems[e.Position].EventId.ToString());
            StartActivity(intent);
        }
        private void EventListView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            System.Console.WriteLine(mItems[e.Position].EventDate);
        }
        private void HandleEvents()
        {
            Globals.sqlconn = new System.Data.SqlClient.SqlConnection(Globals.connsqlstring);
            try
            {
                Globals.sqlconn.Open();
                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand();
                
                cmd.CommandText = "select * from Event_Table inner join Event_Member_Table  on Event_Table.Event_Id like Event_Member_Table.Event_Id where Event_Member_Table.Member_Id like @M_ID and Event_Member_Table.Going like 0";

                /************for testing*********************/
                cmd.Parameters.AddWithValue("@M_ID", 101);
                //cmd.Parameters.AddWithValue("@M_ID", Globals.myID);
                /*******************************************/
                cmd.CommandType = CommandType.Text;
                cmd.Connection = Globals.sqlconn;

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int eventId = (int)reader["Event_Id"];
                    string inviteDate = (string)reader["Event_Date"];
                    string venueName = (string)reader["Event_Name"];
                    string groupName = (string)reader["Venue_Name"];
                    string time = (string)reader["Event_Time"];

                    mListView = FindViewById<ListView>(Resource.Id.eventListView);

                    mItems.Add(new Person() {EventId = eventId, EventDate = inviteDate, GroupName = groupName, EventName = venueName, Time = time });
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Error" + ex, ToastLength.Long).Show();
            }
            finally
            {
                Globals.sqlconn.Close();
            }
        }
        private void FindViews()
        {
            pendingEventListView = FindViewById<ListView>(Resource.Id.pendingEventListView);
        }
    }
}