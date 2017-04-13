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
    public class CreateEventActivity : Activity
    {
        private Button timeButton;
        private Button eventButton;
        private Button eventButton2;
        private Button timeButton2;
        private Button btncreateEvent;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateEvent);
            FindViews();
            HandleEvents();
        }
        private void FindViews()
        {
            btncreateEvent = FindViewById<Button>(Resource.Id.createEvent);
            eventButton = FindViewById<Button>(Resource.Id.eventButton);
            timeButton = FindViewById<Button>(Resource.Id.timeButton);
            eventButton2 = FindViewById<Button>(Resource.Id.eventButton2);
            timeButton2 = FindViewById<Button>(Resource.Id.timeButton2);
        }
        private void HandleEvents()
        {
            eventButton.Click += EventButton_Click;
            timeButton.Click += TimeButton_Click;
            eventButton2.Click += EventButton2_Click;
            timeButton2.Click += TimeButton2_Click;
            btncreateEvent.Click += BtncreateEvent_Click;
        }
        //****************Event Date****************************
        private void EventButton_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                eventButton.Text = time.ToShortDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
            //btncreateEvent.Enabled = true;
        }
        //****************Event Time****************************
        private void TimeButton_Click(object sender, EventArgs e)
        {
            TimePickerFragment frag = TimePickerFragment.NewInstance(delegate (string time)
            {
                timeButton.Text = time;
            });
            frag.Show(FragmentManager, TimePickerFragment.TAG);
        }
        //****************RSVP Date****************************
        private void EventButton2_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                eventButton2.Text = time.ToShortDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }
        //****************RSVP Time****************************
        private void TimeButton2_Click(object sender, EventArgs e)
        {
            TimePickerFragment frag = TimePickerFragment.NewInstance(delegate (string time)
            {
                timeButton2.Text = time;
            });
            frag.Show(FragmentManager, TimePickerFragment.TAG);
        }
        //****************Create Event****************************
        private void BtncreateEvent_Click(object sender, EventArgs e)
        {
            if (eventButton.Text != "" && timeButton.Text != "")
            {
                var intent = new Intent(this, typeof(InviteGuestActivity));
                intent.PutExtra("Date", eventButton.Text);
                intent.PutExtra("Time", timeButton.Text);
                intent.PutExtra("Close_Date", eventButton2.Text);
                intent.PutExtra("Close_Time", timeButton2.Text);
                StartActivity(intent);
            }
            else
            {
                if (eventButton.Text == "")
                {
                    Toast.MakeText(this, "Please choose event date", ToastLength.Long).Show();
                }
                else if(timeButton.Text == "")
                {
                    Toast.MakeText(this, "Please choose event time", ToastLength.Long).Show();
                }
            }           
        }
    }
}