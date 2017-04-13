using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Content.PM;

namespace CountMeIn
{
    [Activity(Label = "Count-Me-In", MainLauncher = true, Icon = "@drawable/logo_sml", ScreenOrientation = ScreenOrientation.Portrait)]
    public class LoginSignUPActivity : Activity
    {
        private Button btnSignIn;
        private Button btnSignUp;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
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