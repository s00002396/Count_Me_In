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
    [Activity(Label = "Create an Event", MainLauncher = false)]
    public class TestCreateEventActivity : Activity
    {
        //private DatePicker datePicker;
        //private Button btnChange;

        private TextView eventDate;
        private TextView eventTime;
        private TextView closeDate;
        private TextView closeTime;

        private Button txtDate;
        private Button textDateClose;
        private Button textEnterTime;
        private Button textTimeClose;

        private Button btncreateEvent;
        private Button btnAddVenue;
        private Spinner spinner;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.TestCreateEvent);
            FindViews();

            HandleEvents();
            SqlConnection sqlconn;
            var adapter2 = string.Format("Server=tcp:dominicbrennan.database.windows.net,1433;Initial Catalog=CountMeIn;Persist Security Info=False;User ID=dominicbrennan;Password=Fld118yi;MultipleActiveResultSets=False;Trusted_Connection=false;Encrypt=false;Connection Timeout=30;");
            sqlconn = new System.Data.SqlClient.SqlConnection(adapter2);
            try
            {
                sqlconn.Open();
                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT Venue_Name FROM Venue_Table ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlconn;

                reader = cmd.ExecuteReader();
                List<String> mylist = new List<String>();

                while (reader.Read())
                {
                    string venueName = (string)reader["Venue_Name"];
                    mylist.Add(venueName);
                }
                spinner.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, mylist);
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
            eventDate = FindViewById<TextView>(Resource.Id.eventDate);
            eventTime = FindViewById<TextView>(Resource.Id.eventTime);
            closeDate = FindViewById<TextView>(Resource.Id.dateClose);
            closeTime = FindViewById<TextView>(Resource.Id.closeTime);

            btncreateEvent = FindViewById<Button>(Resource.Id.createEvent);
            txtDate = FindViewById<Button>(Resource.Id.eventButton);
            textDateClose = FindViewById<Button>(Resource.Id.eventButton2);
            textEnterTime = FindViewById<Button>(Resource.Id.timeButton);
            textTimeClose = FindViewById<Button>(Resource.Id.timeButton2);
            spinner = FindViewById<Spinner>(Resource.Id.spinner);
            btnAddVenue = FindViewById<Button>(Resource.Id.addVenue);
        }

        private void HandleEvents()
        {
            btncreateEvent.Click += BtncreateEvent_Click;
            txtDate.Click += TxtDate_Click;
            textDateClose.Click += TextDateClose_Click;
            textEnterTime.Click += TextEnterTime_Click;
            textTimeClose.Click += TextTimeClose_Click;
            btnAddVenue.Click += BtnAddVenue_Click;
        }

        private void BtnAddVenue_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(AddVenueActivity));
            StartActivity(intent);
        }

        private void TextTimeClose_Click(object sender, EventArgs e)
        {
            TimePickerFragment frag = TimePickerFragment.NewInstance(delegate (string time)
            {
                closeTime.Text = time;
            });
            frag.Show(FragmentManager, TimePickerFragment.TAG);
        }

        private void TextEnterTime_Click(object sender, EventArgs e)
        {
            TimePickerFragment frag = TimePickerFragment.NewInstance(delegate (string time)
            {
                eventTime.Text = time;
            });
            frag.Show(FragmentManager, TimePickerFragment.TAG);
        }

        private void TextDateClose_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                closeDate.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void TxtDate_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                eventDate.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void BtncreateEvent_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(InviteGuestActivity));
            intent.PutExtra("Date", txtDate.Text);
            intent.PutExtra("Venue", spinner.SelectedItem.ToString());
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