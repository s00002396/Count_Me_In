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
    [Activity(Label = "Create Account")]
    public class CreateAccountActivity : Activity
    {
        private Button btnSignUp;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CreateAccount);

            FindViews();

            HandleEvents();
        }

        private void FindViews()
        {
            btnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
           
        }

        private void HandleEvents()
        {
            btnSignUp.Click += BtnSignUp_Click;
        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainMenuActivity));
            
            StartActivity(intent);
        }
    }
}