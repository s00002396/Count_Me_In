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
using CountMeIn.Model;

namespace CountMeIn
{
    [Activity(Label = "Create Account")]
    public class CreateAccountActivity : Activity
    {
        private Button btnSignUp;
        private EditText userName;
        private EditText phoneNumber;
        private EditText password;
        private EditText reenterPwd;

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
            userName = FindViewById<EditText>(Resource.Id.username);
            phoneNumber = FindViewById<EditText>(Resource.Id.phoneNo);
            password = FindViewById<EditText>(Resource.Id.textPassword);
            reenterPwd = FindViewById<EditText>(Resource.Id.renterPwd);
        }
        private void HandleEvents()
        {
            btnSignUp.Click += BtnSignUp_Click;
        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            Globals.sqlconn = new System.Data.SqlClient.SqlConnection(Globals.connsqlstring);
            try
            {
                if (password.Text == reenterPwd.Text)
                {
                    Globals.sqlconn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO Member_Table(Username,PhoneNo,Password,ReEnterPassword) VALUES(@user,@phoneNo, @pass,@reenterpwd)", Globals.sqlconn);

                    cmd.Parameters.AddWithValue("@user", userName.Text);
                    cmd.Parameters.AddWithValue("@phoneNo", phoneNumber.Text);
                    cmd.Parameters.AddWithValue("@pass", password.Text);
                    cmd.Parameters.AddWithValue("@reenterpwd", reenterPwd.Text);
                    cmd.ExecuteNonQuery();
                    Toast.MakeText(this, "Sign Up Complete", ToastLength.Long).Show();
                    var intent = new Intent(this, typeof(LoginActivity));

                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(this, "Passwords must match", ToastLength.Long).Show();
                }                
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Error" + ex, ToastLength.Long).Show();
            }
            finally
            {
                Globals.sqlconn.Close();
            }
        }
    }
}