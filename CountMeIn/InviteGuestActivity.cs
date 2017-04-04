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
    [Activity(Label = "Choose Venue", MainLauncher = false)]
    public class InviteGuestActivity : Activity
    {
        private TextView txtDate;
        private TextView txtTime;
        private EditText txtEventName;
        private Spinner spinner;
        private Button chooseGuests;
        private Button btnSendInvite;
        SqlConnection sqlconn;
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
            //SqlConnection sqlconn;
            var connsqlstring = string.Format("Server=tcp:dominicbrennan.database.windows.net,1433;Initial Catalog=CountMeIn;Persist Security Info=False;User ID=dominicbrennan;Password=Fld118yi;MultipleActiveResultSets=False;Trusted_Connection=false;Encrypt=false;Connection Timeout=30;");
            sqlconn = new System.Data.SqlClient.SqlConnection(connsqlstring);

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
            #endregion
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            venue = string.Format("{0}", spinner.GetItemAtPosition(e.Position));//get the selected venue            
        }

        //****************Choose Guest SAVE???****************************
        private void ChooseGuests_Click(object sender, EventArgs e)
        {            
            #region sql
            sqlconn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlconn;
                
                cmd.CommandText = "INSERT INTO Event_Table(Event_Name, Event_Date, Event_Time,Venue_Name,Close_Date, Close_Time)   VALUES(@param1,@param2,@param3,@param4,@param5,@param6) SELECT SCOPE_IDENTITY()";

                cmd.Parameters.AddWithValue("@param1", txtEventName.Text);
                cmd.Parameters.AddWithValue("@param2", date);//Venue Name  
                cmd.Parameters.AddWithValue("@param3", time);//Group Name Dont Have yet
                cmd.Parameters.AddWithValue("@param4", venue);//Time
                cmd.Parameters.AddWithValue("@param5", closeDate);//Not going by default
                cmd.Parameters.AddWithValue("@param6", closeTime);

                //cmd.ExecuteNonQuery();
                sqlconn.Close();

                sqlconn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                insertedID = reader[0].ToString();

            }
            catch (Exception ex)
            {
                string toast = string.Format("Something went wrong  {0}", ex);
                Toast.MakeText(this, toast, ToastLength.Long).Show();
            }
            finally
            {
                //var dd = insertedID;
                sqlconn.Close();
            }
            #endregion

            var intent = new Intent(this, typeof(GuestInviteActivity));
            intent.PutExtra("New_ID", insertedID);
            StartActivity(intent);
        }
    }
}