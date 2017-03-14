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
    [Activity(Label = "Choose Venue")]
    public class InviteGuestActivity : Activity
    {
        private TextView txtDate;
        private TextView txtTime;
        private Spinner spinner;
        private Button chooseGuests;
        private Button btnSendInvite;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            string date = Intent.GetStringExtra("Date") ?? "Data not available";
            string text = Intent.GetStringExtra("Time") ?? "Data not available";
            SetContentView(Resource.Layout.InviteGuests);

            FindViews();
            //btnSendInvite.FindViewById<Button>(Resource.Id.sendInvite);
            HandleEvents();
            //Intent intent = getIntent();

            //spinner = FindViewById<TextView>(Resource.Id.textVenueName);

            txtDate.Text = date;
            txtTime.Text = text;
            SqlConnection sqlconn;
            var adapter2 = string.Format("Server=tcp:dominicbrennan.database.windows.net,1433;Initial Catalog=CountMeIn;Persist Security Info=False;User ID=dominicbrennan;Password=Fld118yi;MultipleActiveResultSets=False;Trusted_Connection=false;Encrypt=false;Connection Timeout=30;");
            sqlconn = new System.Data.SqlClient.SqlConnection(adapter2);
            try
            {
                sqlconn.Open();
                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT Venue_Name FROM Venue_Table";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlconn;

                reader = cmd.ExecuteReader();
                List<String> mylist = new List<String>();

                while (reader.Read())
                {
                    string venueName = (string)reader["Venue_Name"];
                    mylist.Add(venueName);
                }
                spinner.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, mylist);
            }
            catch (Exception ex)
            {
                string toast = string.Format("Somethinf went wrong  {0}", ex);
                Toast.MakeText(this, toast, ToastLength.Long).Show();
            }
            finally
            {
                sqlconn.Close();
            }
        }

        private void FindViews()
        {
            chooseGuests = FindViewById<Button>(Resource.Id.sendInvite);
            txtDate = FindViewById<TextView>(Resource.Id.textDate);
            txtTime = FindViewById<TextView>(Resource.Id.textTime);
            spinner = FindViewById<Spinner>(Resource.Id.spinner);
        }
        private void HandleEvents()
        {
            chooseGuests.Click += ChooseGuests_Click;
            //btnSendInvite.Click += SendInvite_Click;
        }

        private void ChooseGuests_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(GuestInviteActivity));
            StartActivity(intent);
        }

        private void SendInvite_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(GuestInviteActivity));
            StartActivity(intent);
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            string toast = string.Format("The selected item is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
    }
}