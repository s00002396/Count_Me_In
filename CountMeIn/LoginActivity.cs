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
    [Activity(Label = "Login", MainLauncher = false)]
    public class LoginActivity : Activity
    {
        private Button btnLogIn;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Login);

            FindViews();

            HandleEvents();
        }       

        private void FindViews()
        {
            btnLogIn = FindViewById<Button>(Resource.Id.btnLogIn);
        }

        private void HandleEvents()
        {
            btnLogIn.Click += BtnLogIn_Click;
        }

        private void BtnLogIn_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainMenuActivity));
            StartActivity(intent);
        }
    }
}