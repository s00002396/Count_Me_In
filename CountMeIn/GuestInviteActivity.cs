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
    [Activity(Label = "Invite Guests", MainLauncher = false)]
    public class GuestInviteActivity : Activity
    {
        private ListView guestListView;
        private List<int> invitedGuestID;
        private List<Member> mItems;
        private ListView mListView;
        string new_ID;
        private Button btnInvite;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            #region sql Connection
            //string connsqlstring = string.Format("Server=tcp:dominicbrennan.database.windows.net,1433;Initial Catalog=CountMeIn;Persist Security Info=False;User ID=dominicbrennan;Password=Fld118yi;MultipleActiveResultSets=False;Trusted_Connection=false;Encrypt=false;Connection Timeout=30;");
            Globals.sqlconn = new System.Data.SqlClient.SqlConnection(Globals.connsqlstring);
            #endregion

            new_ID = Intent.GetStringExtra("New_ID") ?? "Data not available";

            SetContentView(Resource.Layout.GuestInvite);
            FindViews();
            invitedGuestID = new List<int>();
            mItems = new List<Member>();
            HandleEvents();

            GuestInviteAdapter adapter = new GuestInviteAdapter(this, mItems);
            guestListView.Adapter = adapter;
            guestListView.ItemClick += EventListView_ItemClick;
        }

        private void EventListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //add Member_Id to a list then use foreach in finish button to update Member_Event_Table in db.
            //var gg = mItems[e.Position];            
            //Toast.MakeText(this, "User ID " + mItems[e.Position].Member_Id, ToastLength.Long).Show();
            Toast.MakeText(this,mItems[e.Position].Member_Phone + " invited ", ToastLength.Long).Show();
            
            invitedGuestID.Add(mItems[e.Position].Member_Id);            
        }

        private void FindViews()
        {
            btnInvite = FindViewById<Button>(Resource.Id.createInvite);
            guestListView = FindViewById<ListView>(Resource.Id.guestListView);
        }

        private void HandleEvents()
        {
            try
            {
                Globals.sqlconn.Open();

                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand();

                //cmd.CommandText = "SELECT Username,PhoneNo,Password FROM Member_Table";
                cmd.CommandText = "SELECT * FROM Member_Table";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = Globals.sqlconn;

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int eventId = (int)reader["Member_Id"];
                    string userName = (string)reader["Username"];
                    string phone = (string)reader["PhoneNo"];
                    string password = (string)reader["Password"];

                    mListView = FindViewById<ListView>(Resource.Id.guestListView);

                    mItems.Add(new Member() {Member_Id = eventId, Member_Name = "Guest Details", Member_Phone = userName, GroupName = phone, UserName = "+" });
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
            btnInvite.Click += BtnInvite_Click;
        }

        private void BtnInvite_Click(object sender, EventArgs e)
        {
            #region sql
            Globals.sqlconn.Open();
            try
            {                
                SqlCommand cmd = new SqlCommand();
                
                int i = 20;
                foreach (var item in invitedGuestID)
                {
                    
                    cmd.Connection = Globals.sqlconn;
                    cmd.CommandText = "INSERT INTO Event_Member_Table(Member_Id, Event_Id, Going)   VALUES(@paramA" + i+ ",@paramB"+ i+",@paramC"+ i+")";

                    cmd.Parameters.AddWithValue("@paramA" + i, Convert.ToInt32(item));//MemberID
                    cmd.Parameters.AddWithValue("@paramB" +i, new_ID);//EventID  
                    cmd.Parameters.AddWithValue("@paramC" + i, 0);//Not going by default.
                    cmd.ExecuteNonQuery();
                    i++;
                } 
            }
            catch (Exception ex)
            {
                string toast = string.Format("Something went wrong  {0}", ex);
                Toast.MakeText(this, toast, ToastLength.Long).Show();
            }
            finally
            {
                Globals.sqlconn.Close();
            }
            #endregion

            Notification.Builder builder = new Notification.Builder(this)
            .SetContentTitle("Count-Me-In")
            .SetContentText("You have a new invite")
            .SetSmallIcon(Resource.Drawable.Icon);

            Notification notification = builder.Build();
            NotificationManager notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;

            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
            
            var intent = new Intent(this, typeof(MainMenuActivity));
            StartActivity(intent);
        }
    }
}