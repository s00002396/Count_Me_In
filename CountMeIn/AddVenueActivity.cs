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
    [Activity(Label = "Add Venue")]
    public class AddVenueActivity : Activity
    {
        private Button btnCreateVenueButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AddVenue);

            FindViews();

            HandleEvents();
        }
        private void FindViews()
        {
            btnCreateVenueButton = FindViewById<Button>(Resource.Id.createVenue);
            //btnCreateDB = FindViewById<Button>(Resource.Id.aboutButton);
            //txtSysLog = FindViewById<TextView>(Resource.Id.txtSysLog);
        }

        private void HandleEvents()
        {
            btnCreateVenueButton.Click += BtnCreateVenueButton_Click;
            //btnCreateDB.Click += BtnCreateDB_Click;
        }

        private void BtnCreateVenueButton_Click(object sender, EventArgs e)
        {
            //var dialog = new AlertDialog.Builder(this);
            //dialog.SetTitle("Congirmation");
            //dialog.SetMessage("Venue was added");
            //dialog.Show();
           
            var intent = new Intent(this, typeof(CreateEventActivity));
            StartActivity(intent);
        }
    }
}