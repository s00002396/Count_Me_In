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
    [Activity(Label = "Login", MainLauncher = true, Icon = "@drawable/logo")]
    public class LoginActivity : Activity
    {
        private Button btnLogIn;
        private EditText username;
        private EditText pword;
        private bool flag = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Login);
            Android.Telephony.TelephonyManager tMgr = (Android.Telephony.TelephonyManager)this.GetSystemService(Android.Content.Context.TelephonyService);
            string mPhoneNumber = tMgr.Line1Number.Substring(4);
            Globals.myPhoneNumber = mPhoneNumber.Insert(0, "0");
            FindViews();
            HandleEvents();
        }  
        private void FindViews()
        {
            btnLogIn = FindViewById<Button>(Resource.Id.btnLogIn);
            username = FindViewById<EditText>(Resource.Id.loginUserName);
            pword = FindViewById<EditText>(Resource.Id.loginPassword);
        }
        private void HandleEvents()
        {
            btnLogIn.Click += BtnLogIn_Click;
        }
        private void BtnLogIn_Click(object sender, EventArgs e)
        {
            Globals.sqlconn = new System.Data.SqlClient.SqlConnection(Globals.connsqlstring);
            try
            {
                Globals.sqlconn.Open();

                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "SELECT Member_Id,Username,PhoneNo,Password FROM Member_Table WHERE PhoneNo LIKE @PhoneNumber";
                cmd.Parameters.AddWithValue("@PhoneNumber", Globals.myPhoneNumber);
                
                cmd.CommandType = CommandType.Text;
                cmd.Connection = Globals.sqlconn;

                reader = cmd.ExecuteReader();
               
                while (reader.Read())
                {
                    Globals.myID = (int)reader["Member_Id"];
                    int memberId = (int)reader["Member_Id"];
                    string userName = (string)reader["Username"];
                    string phone = (string)reader["PhoneNo"];
                    string password = (string)reader["Password"];
                    if (((userName == username.Text || phone == username.Text)) && password == pword.Text)
                    {
                        flag = true;
                        var intent = new Intent(this, typeof(MainMenuActivity));
                        StartActivity(intent);                                               
                    }
                    if(flag==false)
                    {
                        Toast.MakeText(this, "Invalid username or password", ToastLength.Long).Show();
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Error" + ex, ToastLength.Long).Show();
            }
            finally
            {
                Globals.sqlconn.Close();
                flag = false;
            }
        }
    }
}