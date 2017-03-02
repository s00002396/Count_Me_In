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

namespace CountMeIn
{
    [Activity(Label = "Create an Event")]
    public class CreateEventActivity : Activity
    {
        private DatePicker datePicker;
        private Button btnChange;
        private TextView txtDate;
        private Button btncreateEvent;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CreateEvent);


            FindViews();

            HandleEvents();


            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);

            //spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.planets_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            // datePicker = FindViewById<DatePicker>(Resource.Id.datePicker);
            //btnChange = FindViewById<Button>(Resource.Id.change_date_button);
            //txtDate = FindViewById<TextView>(Resource.Id.txtViewDate);

            //txtDate.Text = (getDate());
            //btnChange.Click += BtnChange_Click;

        }

        private void FindViews()
        {
            btncreateEvent = FindViewById<Button>(Resource.Id.createEvent);
        }

        private void HandleEvents()
        {
            btncreateEvent.Click += BtncreateEvent_Click;
        }

        private void BtncreateEvent_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(InviteGuestActivity));
            StartActivity(intent);
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            string toast = string.Format("The planet is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        //private void BtnChange_Click(object sender, EventArgs e)
        //{
        //    txtDate.Text = getDate();
        //}

        //private string getDate()
        //{
        //    StringBuilder strCurrentDate = new StringBuilder();
        //    int month = datePicker.Month + 1;
        //    strCurrentDate.Append("Date : " + month + "/" + datePicker.DayOfMonth + "/" + datePicker.Year);
        //    return strCurrentDate.ToString();
        //}
    }
}