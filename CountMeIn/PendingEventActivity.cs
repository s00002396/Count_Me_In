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
    [Activity(Label = "Pending Invites", MainLauncher = false)]
    public class PendingEventActivity : Activity
    {
        private ListView eventListView;

        private List<Person> mItems;
        private ListView mListView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PendingEventView);
            FindViews();

            mItems = new List<Person>();

            HandleEvents();

            MyListViewAdapter adapter = new MyListViewAdapter(this, mItems);
            eventListView.Adapter = adapter;
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

                cmd.CommandText = "SELECT * FROM Invite_Table WHERE Going like 0";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlconn;

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string inviteDate = (string)reader["Invite_Date"];
                    string venueName = (string)reader["Venue_Name"];
                    string groupName = (string)reader["Group_Name"];
                    string time = (string)reader["Time"];

                    mListView = FindViewById<ListView>(Resource.Id.eventListView);

                    mItems.Add(new Person() { EventDate = inviteDate, GroupName = groupName, EventName = venueName, Time = time });
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
            eventListView = FindViewById<ListView>(Resource.Id.eventListView);
        }
    }
}