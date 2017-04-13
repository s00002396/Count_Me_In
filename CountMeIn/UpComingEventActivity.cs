using System;
using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;
using CountMeIn.Model;
using System.Data.SqlClient;
using System.Data;
using ListViewEvents;

namespace CountMeIn
{
    [Activity(Label = "UpComing Event", MainLauncher = false)]
    public class UpComingEventActivity : Activity
    {
        private ListView eventListView;
        private List<Person> mItems;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.UpComingEvent);
            FindViews();
            var m_ID = Globals.myPhoneNumber;
            var test = Globals.myID;
            mItems = new List<Person>();

            HandleEvents();

            MyListViewAdapter adapter = new MyListViewAdapter(this, mItems);
            eventListView.Adapter = adapter;

            eventListView.ItemClick += EventListView_ItemClick;
        }        

        private void EventListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, "User ID " + mItems[e.Position].EventId, ToastLength.Long).Show();

        }

        private void FindViews()
        {
            eventListView = FindViewById<ListView>(Resource.Id.eventListView);
        }       

        private void HandleEvents()
        {
            Globals.sqlconn = new System.Data.SqlClient.SqlConnection(Globals.connsqlstring);
            try
            {
                Globals.sqlconn.Open();

                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand();               

                cmd.CommandText = "select * from Event_Table inner join Event_Member_Table on Event_Table.Event_Id like Event_Member_Table.Event_Id where Event_Member_Table.Member_Id like @M_ID and Event_Member_Table.Going like 1";
                /************for testing*********************/
                cmd.Parameters.AddWithValue("@M_ID", 101);
                //cmd.Parameters.AddWithValue("@M_ID", Globals.myID);
                /********************************************/
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

                    mItems.Add(new Person() { EventId = eventId, EventDate = inviteDate, GroupName = groupName, EventName = venueName, Time = time });
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
    }
}