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
using System.Data.SqlClient;
using System.Data;
using ListViewEvents;
using CountMeIn.Adapters;

namespace CountMeIn
{
    [Activity(Label = "Invite Guests", MainLauncher = true)]
    public class GuestInviteActivity : Activity
    {
        private ListView guestListView;

        private List<Member> mItems;
        private ListView mListView;
        string new_ID;
        private Button btnInvite;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            new_ID = Intent.GetStringExtra("New_ID") ?? "Data not available";

            SetContentView(Resource.Layout.GuestInvite);
            FindViews();
            mItems = new List<Member>();
            HandleEvents();

            GuestInviteAdapter adapter = new GuestInviteAdapter(this, mItems);
            guestListView.Adapter = adapter;
            guestListView.ItemClick += EventListView_ItemClick;
        }

        private void EventListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, "User ID " + mItems[e.Position].Member_Id, ToastLength.Long).Show();
        }

        private void FindViews()
        {
            //btnInvite = FindViewById<Button>(Resource.Id.createInvite);
            guestListView = FindViewById<ListView>(Resource.Id.guestListView);
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

                //cmd.CommandText = "SELECT Username,PhoneNo,Password FROM Member_Table";
                cmd.CommandText = "SELECT * FROM Member_Table";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlconn;

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int eventId = (int)reader["Member_Id"];
                    string userName = (string)reader["Username"];
                    string phone = (string)reader["PhoneNo"];
                    string password = (string)reader["Password"];

                    mListView = FindViewById<ListView>(Resource.Id.guestListView);

                    mItems.Add(new Member() {Member_Id = eventId, Member_Name = "Details", Member_Phone = userName, GroupName = phone, UserName = "Invite" });
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Error", ToastLength.Long).Show();
            }
            finally
            {
                sqlconn.Close();
            }
            //btnInvite.Click += BtnInvite_Click;
        }

        //private void BtnInvite_Click(object sender, EventArgs e)
        //{
        //    Notification.Builder builder = new Notification.Builder(this)
        //    .SetContentTitle("Count-Me-In")
        //    .SetContentText("You have a new invite")
        //    .SetSmallIcon(Resource.Drawable.Icon);

        //    Notification notification = builder.Build();
        //    NotificationManager notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;

        //    const int notificationId = 0;
        //    notificationManager.Notify(notificationId, notification);

        //    //Toast.MakeText(this, "Invite Sent ", ToastLength.Long).Show();
        //    //var intent = new Intent(this, typeof(MainMenuActivity));
        //    //StartActivity(intent);
        //}
    }
}