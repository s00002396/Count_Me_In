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
    
    [Activity(Label = "Create an Event", MainLauncher = false)]
    public class CreateEventActivity : Activity
    {
        private DatePicker datePicker;
        private Button btnChange;
        private TextView txtDate;
        private TextView textDateClose;
        private TextView textEnterTime;
        private TextView textTimeClose;
        private Button btncreateEvent;
        private Button btnAddVenue;
        private Spinner spinner;

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
            txtDate = FindViewById<TextView>(Resource.Id.textDate);
            textDateClose = FindViewById<TextView>(Resource.Id.textDateClose);
            textEnterTime = FindViewById<TextView>(Resource.Id.textEnterTime);
            textTimeClose = FindViewById<TextView>(Resource.Id.textTimeClose);
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
                textTimeClose.Text = time;
            });
            frag.Show(FragmentManager, TimePickerFragment.TAG);
        }

        private void TextEnterTime_Click(object sender, EventArgs e)
        {
            TimePickerFragment frag = TimePickerFragment.NewInstance(delegate (string time)
            {
                textEnterTime.Text = time;
            });
            frag.Show(FragmentManager, TimePickerFragment.TAG);
        }

        private void TextDateClose_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                textDateClose.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void TxtDate_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                txtDate.Text = time.ToLongDateString();
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