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
    [Activity(Label = "Main Menu")]
    public class MainMenuActivity : Activity
    {
        private Button btnCreateEventButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.MainMenu);

            FindViews();

            HandleEvents();
        }

        private void FindViews()
        {
            btnCreateEventButton = FindViewById<Button>(Resource.Id.createEventButton);
        }

        private void HandleEvents()
        {
            btnCreateEventButton.Click += BtnCreateEventButton_Click;
        }

        private void BtnCreateEventButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CreateEventActivity));
            StartActivity(intent);
        }
    }
}