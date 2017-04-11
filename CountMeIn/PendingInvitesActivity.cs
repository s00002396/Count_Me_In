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
using CountMeIn.Model;
using CountMeIn.Adapters;

namespace CountMeIn
{
    [Activity(Label = "Invite Details", MainLauncher = true)]
    public class PendingInvitesActivity : Activity
    {
        //SqlConnection sqlconn;
        string eventDate;
        string eventName;
        string eventID;
        private List<Member> mItems;
        private ListView guestListView;
        private ListView mListView;
        private TextView txtEventDate;
        private TextView txtEventName;
        private Button sendInvite;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            #region sql Connection
            Globals.sqlconn = new System.Data.SqlClient.SqlConnection(Globals.connsqlstring);
            #endregion
            SetContentView(Resource.Layout.PendingInvites);
            eventID = Intent.GetStringExtra("Event_Id") ?? "Data not available";
            eventDate = Intent.GetStringExtra("Event Date") ?? "Data not available";
            eventName = Intent.GetStringExtra("Event Venue") ?? "Data not available";
            FindViews();
            var test = Globals.myID;
            mItems = new List<Member>();
            HandleEvents();
            GuestInviteAdapter adapter = new GuestInviteAdapter(this, mItems);
            guestListView.Adapter = adapter;

            txtEventDate.Text = eventDate;
            txtEventName.Text = eventName;            
        }

        private void FindViews()
        {
            txtEventDate = FindViewById<TextView>(Resource.Id.txtEventDate);
            txtEventName = FindViewById<TextView>(Resource.Id.txtEventName);
            sendInvite = FindViewById<Button>(Resource.Id.sendInvite);
            guestListView = FindViewById<ListView>(Resource.Id.guestListView);
        }

        private void HandleEvents()
        {
            try
            {
                Globals.sqlconn.Open();

                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand();
                
                cmd.CommandText = "SELECT * FROM Member_Table inner join Event_Member_Table on Member_Table.Member_Id like Event_Member_Table.Member_Id where Event_Member_Table.Event_Id like @M_ID and Event_Member_Table.Going like 1";

                cmd.Parameters.AddWithValue("@M_ID", eventID);

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

                    mItems.Add(new Member() { Member_Id = eventId, Member_Name = "Guest Details", Member_Phone = userName, GroupName = phone, UserName = "" });
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
            sendInvite.Click += SendInvite_Click;
        }
        private void SendInvite_Click(object sender, EventArgs e)
        {
            try
            {
                Globals.sqlconn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Globals.sqlconn;
                cmd.CommandText = "UPDATE Event_Member_Table SET Going=@Going where Event_Member_Table.Member_Id like 101 and Event_Member_Table.Event_Id like @E_ID ";
                cmd.Parameters.AddWithValue("@E_ID", eventID);
                cmd.Parameters.AddWithValue("@Going", 1);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Error" + ex, ToastLength.Long).Show();
            }
            finally
            {
                Globals.sqlconn.Close();
                Toast.MakeText(this, "Invite accepted ", ToastLength.Long).Show();
                var intent = new Intent(this, typeof(PendingEventActivity));
                StartActivity(intent);
            }
        }
    }
}