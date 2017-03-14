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
    [Activity(Label = "Create Group")]
    public class CreateGroupActivity : Activity
    {
        private Button btnCreateGroup;
        private EditText username;
        private EditText pword;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

           SetContentView(Resource.Layout.CreateGroup);

            FindViews();

            HandleEvents();
        }
        private void FindViews()
        {
            btnCreateGroup = FindViewById<Button>(Resource.Id.createGroup);
            username = FindViewById<EditText>(Resource.Id.loginUserName);
            pword = FindViewById<EditText>(Resource.Id.loginPassword);
        }

        private void HandleEvents()
        {
            btnCreateGroup.Click += BtnCreateGroup_Click;
        }

        private void BtnCreateGroup_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Group Created", ToastLength.Long).Show();
            var intent = new Intent(this, typeof(MainMenuActivity));
            StartActivity(intent);
        }
    }
}