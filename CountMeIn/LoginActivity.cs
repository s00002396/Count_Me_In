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

namespace CountMeIn
{
    [Activity(Label = "Login", MainLauncher = false)]
    public class LoginActivity : Activity
    {
        private Button btnLogIn;
        private EditText username;
        private EditText pword;

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
            username = FindViewById<EditText>(Resource.Id.loginUserName);
            pword = FindViewById<EditText>(Resource.Id.loginPassword);
        }

        private void HandleEvents()
        {
            btnLogIn.Click += BtnLogIn_Click;
        }

        private void BtnLogIn_Click(object sender, EventArgs e)
        {
            SqlConnection sqlconn;

            string connsqlstring = string.Format("Server=tcp:dominicbrennan.database.windows.net,1433;Initial Catalog=CountMeIn;Persist Security Info=False;User ID=dominicbrennan;Password=Fld118yi;MultipleActiveResultSets=False;Trusted_Connection=false;Encrypt=false;Connection Timeout=30;");
            sqlconn = new System.Data.SqlClient.SqlConnection(connsqlstring);
            try
            {
                sqlconn.Open();

                SqlDataReader reader;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT Username,PhoneNo,Password FROM Member_Table WHERE Member_Id LIKE 101";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlconn;

                reader = cmd.ExecuteReader();
               
                while (reader.Read())
                {
                    string userName = (string)reader["Username"];
                    string phone = (string)reader["PhoneNo"];
                    string password = (string)reader["Password"];
                    //var test = x.GetSqlValues(0);
                    if (((userName == username.Text || phone == username.Text)) && password == pword.Text)
                    {
                        var intent = new Intent(this, typeof(MainMenuActivity));
                        StartActivity(intent);                        
                    }
                    else
                    {
                        Toast.MakeText(this, "Invalid username or password", ToastLength.Long).Show();
                    }
                }
                //txtSysLog.Text = "Success Finally";
            }
            catch (Exception ex)
            {
                //txtSysLog.Text = ex.ToString();
            }
            finally
            {
                sqlconn.Close();
            }

            //var intent = new Intent(this, typeof(MainMenuActivity));
            //StartActivity(intent);
        }
    }
}