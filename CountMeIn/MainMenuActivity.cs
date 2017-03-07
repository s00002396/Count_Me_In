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
using CountMeIn.ORM;
using MySql.Data.MySqlClient;
using System.Data;

namespace CountMeIn
{
    [Activity(Label = "Main Menu", MainLauncher = false)]
    public class MainMenuActivity : Activity
    {
        private Button btnCreateEventButton;
        private Button btnCreateDB;
        private TextView txtSysLog;

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
            btnCreateDB = FindViewById<Button>(Resource.Id.aboutButton);
            txtSysLog = FindViewById<TextView>(Resource.Id.txtSysLog);
        }

        private void HandleEvents()
        {
            btnCreateEventButton.Click += BtnCreateEventButton_Click;
            btnCreateDB.Click += BtnCreateDB_Click;
        }

        private void BtnCreateDB_Click(object sender, EventArgs e)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=127.0.0.1;uid=root;" +
    "pwd=Fld118yi;database=test;";



            //MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection();
            //MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
            //string SQL;
            //conn.ConnectionString = "Server=127.0.0.1;Database=world;Uid=root;Pwd=Fld118yi";
            //MySqlConnection con = new MySqlConnection("server=localhost;userid=root;database=world");
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

            }
            catch (MySqlException ex)
            {

                txtSysLog.Text = ex.ToString();
            }
            finally
            {
               // conn.Close();
            }
            //DBRepository dbr = new DBRepository();
            //var result = dbr.CreateDB();
            //Toast.MakeText(this, result, ToastLength.Short).Show();
        }

        private void BtnCreateEventButton_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CreateEventActivity));
            StartActivity(intent);
        }
    }
}