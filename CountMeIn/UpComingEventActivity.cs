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

namespace CountMeIn
{
    [Activity(Label = "UpComing Event", MainLauncher = false)]
    public class UpComingEventActivity : Activity
    {
        private ListView eventListView;
        //string FirstName = "Bobby";
        private List<Person> mItems;
        //private ListView mListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.UpComingEvent);

            FindViews();
            var m_ID = Globals.myPhoneNumber;

            mItems = new List<Person>();

            HandleEvents();

            MyListViewAdapter adapter = new MyListViewAdapter(this, mItems);
            eventListView.Adapter = adapter;

            eventListView.ItemClick += EventListView_ItemClick;
            
            //eventListView.ItemClick += EventListView_ItemClick;
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
            //SqlConnection sqlconn;
            //string connsqlstring = string.Format("Server=tcp:dominicbrennan.database.windows.net,1433;Initial Catalog=CountMeIn;Persist Security Info=False;User ID=dominicbrennan;Password=Fld118yi;MultipleActiveResultSets=False;Trusted_Connection=false;Encrypt=false;Connection Timeout=30;");
            Globals.sqlconn = new System.Data.SqlClient.SqlConnection(Globals.connsqlstring);
            try
            {
                Globals.sqlconn.Open();

                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand();

                #region Existing Code
                //cmd.CommandText = "SELECT * FROM Invite_Table WHERE Going like 1";
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

                cmd.CommandText = "select * from Event_Table inner join Event_Member_Table  on Event_Table.Event_Id like Event_Member_Table.Event_Id where Event_Member_Table.Member_Id like @M_ID and Event_Member_Table.Going like 1";
                cmd.Parameters.AddWithValue("@M_ID", 101);
                // cmd.Parameters.AddWithValue("@M_ID", Globals.s_Name);
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