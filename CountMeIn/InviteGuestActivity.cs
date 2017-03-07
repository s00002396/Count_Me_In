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
    [Activity(Label = "Invite Guests", MainLauncher = false)]
    public class InviteGuestActivity : Activity
    {
        private TextView txtDate;
        private TextView spinner;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            string date = Intent.GetStringExtra("Date") ?? "Data not available";
            string text = Intent.GetStringExtra("Venue") ?? "Data not available";
            SetContentView(Resource.Layout.InviteGuests);
            //Intent intent = getIntent();
            txtDate = FindViewById<TextView>(Resource.Id.textTime);
            spinner = FindViewById<TextView>(Resource.Id.textVenueName);

            txtDate.Text = date;
            spinner.Text = text;
        }
    }
}