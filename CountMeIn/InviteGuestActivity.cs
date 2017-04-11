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

namespace CountMeIn
{
    [Activity(Label = "Choose Venue", MainLauncher = false)]
    public class InviteGuestActivity : Activity
    {
        private TextView txtDate;
        private TextView txtTime;
        private EditText txtEventName;
        private Spinner spinner;
        private Button chooseGuests;
        private Button btnSendInvite;
        //SqlConnection sqlconn;
        string venue;
        string date;
        string time;
        string closeDate;
        string closeTime;
        string newID;
        string insertedID;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //var connsqlstring = string.Format("Server=tcp:dominicbrennan.database.windows.net,1433;Initial Catalog=CountMeIn;Persist Security Info=False;User ID=dominicbrennan;Password=Fld118yi;MultipleActiveResultSets=False;Trusted_Connection=false;Encrypt=false;Connection Timeout=30;");
            Globals.sqlconn = new System.Data.SqlClient.SqlConnection(Globals.connsqlstring);
            //sqlconn = new System.Data.SqlClient.SqlConnection(Globals.connsqlstring);
            date = Intent.GetStringExtra("Date") ?? "Data not available";
            time = Intent.GetStringExtra("Time") ?? "Data not available";
            closeDate = Intent.GetStringExtra("Close_Date") ?? "Data not available";
            closeTime = Intent.GetStringExtra("Close_Time") ?? "Data not available";

            SetContentView(Resource.Layout.InviteGuests);

            FindViews();
            HandleEvents();

            txtDate.Text = date;
            txtTime.Text = time;
        }

        private void FindViews()
        {
            chooseGuests = FindViewById<Button>(Resource.Id.sendInvite);
            txtDate = FindViewById<TextView>(Resource.Id.textDate);
            txtTime = FindViewById<TextView>(Resource.Id.textTime);
            spinner = FindViewById<Spinner>(Resource.Id.spinner);
            txtEventName = FindViewById<EditText>(Resource.Id.txtEventName);
        }
        private void HandleEvents()
        {
            chooseGuests.Click += ChooseGuests_Click;
            //btnSendInvite.Click += SendInvite_Click;
            spinner.ItemSelected += Spinner_ItemSelected;

            #region SqlConnection (Get the Venue)            
            try
            {
                Globals.sqlconn.Open();
                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT Venue_Name FROM Venue_Table";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = Globals.sqlconn;

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
                string toast = string.Format("Something went wrong  {0}", ex);
                Toast.MakeText(this, toast, ToastLength.Long).Show();
            }
            finally
            {
                Globals.sqlconn.Close();
            }
            #endregion
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            venue = string.Format("{0}", spinner.GetItemAtPosition(e.Position));//get the selected venue            
        }

        //****************Choose Guest Create Event On db****************************
        private void ChooseGuests_Click(object sender, EventArgs e)
        {
            #region Create the event on the db
            if (venue != "Select Venue" && txtEventName.Text != "")
            {               
                Globals.sqlconn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = Globals.sqlconn;

                    cmd.CommandText = "INSERT INTO Event_Table(Event_Name, Event_Date, Event_Time,Venue_Name,Close_Date, Close_Time)   VALUES(@param1,@param2,@param3,@param4,@param5,@param6) SELECT SCOPE_IDENTITY()";

                    cmd.Parameters.AddWithValue("@param1", txtEventName.Text);
                    cmd.Parameters.AddWithValue("@param2", date);//Venue Name  
                    cmd.Parameters.AddWithValue("@param3", time);//Group Name Dont Have yet
                    cmd.Parameters.AddWithValue("@param4", venue);//Time
                    cmd.Parameters.AddWithValue("@param5", closeDate);//Not going by default
                    cmd.Parameters.AddWithValue("@param6", closeTime);

                    Globals.sqlconn.Close();

                    Globals.sqlconn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    insertedID = reader[0].ToString();
                }
                catch (Exception ex)
                {
                    string toast2 = string.Format("Something went wrong  {0}", ex);
                    Toast.MakeText(this, toast2, ToastLength.Long).Show();
                }
                finally
                {
                    Globals.sqlconn.Close();
                    var intent = new Intent(this, typeof(GuestInviteActivity));
                    intent.PutExtra("New_ID", insertedID);
                    StartActivity(intent);
                }
            }
            else
            {
                if (venue == "Select Venue" && txtEventName.Text == "")
                {
                    Toast.MakeText(this, "Need to choose a name and select a venue", ToastLength.Long).Show();
                }
                else if (venue == "Select Venue")
                {
                    Toast.MakeText(this, "Need to select a venue", ToastLength.Long).Show();
                }
                else if (txtEventName.Text == "")
                {
                    Toast.MakeText(this, "Need to name event", ToastLength.Long).Show();
                }
                //Toast.MakeText(this, "Need to fill in all details", ToastLength.Long).Show();
            }            
            #endregion

            //var intent = new Intent(this, typeof(GuestInviteActivity));
            //intent.PutExtra("New_ID", insertedID);
            //StartActivity(intent);
        }
    }
}