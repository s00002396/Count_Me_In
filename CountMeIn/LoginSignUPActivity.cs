using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace CountMeIn
{
    [Activity(Label = "Count-Me-In", MainLauncher = false, Icon = "@drawable/icon")]
    public class LoginSignUPActivity : Activity
    {
        private Button btnSignIn;
        private Button btnSignUp;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.LoginSignUP);

            FindViews();

            HandleEvents();
        }       

        private void FindViews()
        {
            btnSignIn = FindViewById<Button>(Resource.Id.btnSignIn);
            btnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
        }

        private void HandleEvents()
        {
            btnSignIn.Click += BtnSignIn_Click;
            btnSignUp.Click += BtnSignUp_Click;
        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CreateAccountActivity));
            StartActivity(intent);
        }

        private void BtnSignIn_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(LoginActivity));
            StartActivity(intent);
        }
    }
}