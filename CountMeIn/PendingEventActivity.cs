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
//using CountMeIn.Adapters;
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

            mItems = new List<Person>();

            //mItems.Add(new Person() { EventDate = "Joe", GroupName = "Smith", EventName = "071557412", Time = "+" });
            //mItems.Add(new Person() { EventDate = "Tom", GroupName = "Tom", EventName = "35", Time = "Male" });
            //mItems.Add(new Person() { EventDate = "Sally", GroupName = "Susan", EventName = "88", Time = "Female" });

            HandleEvents();

            MyPendingEventListAdapter adapter = new MyPendingEventListAdapter(this, mItems);

            pendingEventListView.Adapter = adapter;

            pendingEventListView.ItemLongClick += EventListView_ItemLongClick;
            pendingEventListView.ItemClick += PendingEventListView_ItemClick;
        }

        private void PendingEventListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, "Error" + mItems[e.Position].EventId, ToastLength.Long).Show();
            Console.WriteLine(mItems[e.Position].EventDate);
        }

        private void EventListView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            System.Console.WriteLine(mItems[e.Position].EventDate);
        }

        private void HandleEvents()
        {
            SqlConnection sqlconn;
            string connsqlstring = string.Format("Server=tcp:dominicbrennan.database.windows.net,1433;Initial Catalog=CountMeIn;Persist Security Info=False;User ID=dominicbrennan;Password=Fld118yi;MultipleActiveResultSets=False;Trusted_Connection=false;Encrypt=false;Connection Timeout=30;");
            sqlconn = new System.Data.SqlClient.SqlConnection(connsqlstring);
            try
            {
                sqlconn.Open();

                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand();

                #region Existing COde
                //cmd.CommandText = "SELECT * FROM Invite_Table WHERE Going like 0";
                //cmd.CommandType = CommandType.Text;
                //cmd.Connection = sqlconn;

                //reader = cmd.ExecuteReader();

                //while (reader.Read())
                //{
                //    string inviteDate = (string)reader["Invite_Date"];
                //    string venueName = (string)reader["Venue_Name"];
                //    string groupName = (string)reader["Group_Name"];
                //    string time = (string)reader["Time"];

                //    mListView = FindViewById<ListView>(Resource.Id.eventListView);

                //    mItems.Add(new Person() { EventDate = inviteDate, GroupName = groupName, EventName = venueName, Time = time });
                //}
                #endregion
                cmd.CommandText = "select * from Event_Table inner join Event_Member_Table  on Event_Table.Event_Id like Event_Member_Table.Event_Id where Event_Member_Table.Member_Id like @M_ID and Event_Member_Table.Going like 0";
                cmd.Parameters.AddWithValue("@M_ID", 101);
                // cmd.Parameters.AddWithValue("@M_ID", Globals.s_Name);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlconn;

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
                sqlconn.Close();
            }
        }
        private void FindViews()
        {
            pendingEventListView = FindViewById<ListView>(Resource.Id.pendingEventListView);
        }
    }
}