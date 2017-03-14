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
    [Activity(Label = "Create Account")]
    public class CreateAccountActivity : Activity
    {
        private Button btnSignUp;
        private EditText userName;
        private EditText phoneNumber;
        private EditText password;
        private EditText reenterPwd;
        private TextView txtSysLog;

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
            //txtSysLog = FindViewById<TextView>(Resource.Id.txtSysLog);
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
            SqlConnection sqlconn;
            
            string connsqlstring = string.Format("Server=tcp:dominicbrennan.database.windows.net,1433;Initial Catalog=CountMeIn;Persist Security Info=False;User ID=dominicbrennan;Password=Fld118yi;MultipleActiveResultSets=False;Trusted_Connection=false;Encrypt=false;Connection Timeout=30;");
            sqlconn = new System.Data.SqlClient.SqlConnection(connsqlstring);
            try
            {
                sqlconn.Open();
                
                SqlCommand cmd = new SqlCommand("INSERT INTO Member_Table(Username,PhoneNo,Password,ReEnterPassword) VALUES(@user,@phoneNo, @pass,@reenterpwd)", sqlconn);               

                cmd.Parameters.AddWithValue("@user", userName.Text);
                cmd.Parameters.AddWithValue("@phoneNo", phoneNumber.Text);
                cmd.Parameters.AddWithValue("@pass", password.Text);
                cmd.Parameters.AddWithValue("@reenterpwd", reenterPwd.Text);
                cmd.ExecuteNonQuery();
                Toast.MakeText(this, "Sign Up Complete", ToastLength.Long).Show();
                var intent = new Intent(this, typeof(LoginActivity));

                StartActivity(intent);

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