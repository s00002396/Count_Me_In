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
using CountMeIn.Model;
using CountMeIn.Service;
using System.Data.SqlClient;
using System.Data;

namespace CountMeIn
{
    [Activity(Label = "UpComingRowActivity")]
    public class UpComingRowActivity : Activity
    {
        //private ListView eventListView;
        //private List<Event> allEvents;
        //private EventDataService eventDataService;
        //private TextView hotDogNameTextView;
        //private TextView eventDateView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.UpComingEventRowView);

            //    eventDataService = new EventDataService();

            //    allEvents = eventDataService.GetAllEvents();
            //    FindViews();

            //    //HandleEvents();
            //    #region SQL CONN
            //    SqlConnection sqlconn;

            //    string connsqlstring = string.Format("Server=tcp:dominicbrennan.database.windows.net,1433;Initial Catalog=CountMeIn;Persist Security Info=False;User ID=dominicbrennan;Password=Fld118yi;MultipleActiveResultSets=False;Trusted_Connection=false;Encrypt=false;Connection Timeout=30;");
            //    sqlconn = new System.Data.SqlClient.SqlConnection(connsqlstring);
            //    try
            //    {
            //        sqlconn.Open();

            //        SqlDataReader reader;
            //        SqlCommand cmd = new SqlCommand();
            //        cmd.CommandText = "SELECT Username,PhoneNo,Password FROM Member_Table WHERE Member_Id LIKE 101";

            //        cmd.CommandType = CommandType.Text;
            //        cmd.Connection = sqlconn;

            //        reader = cmd.ExecuteReader();

            //        while (reader.Read())
            //        {
            //            string userName = (string)reader["Username"];
            //            string phone = (string)reader["PhoneNo"];
            //            string password = (string)reader["Password"];
            //            hotDogNameTextView.Text = userName;
            //            hotDogImageView.Text = userName;
            //            //if (((userName == username.Text || phone == username.Text)) && password == pword.Text)
            //            //{
            //            //    //sqlconn.Close();
            //            //    var intent = new Intent(this, typeof(MainMenuActivity));
            //            //    StartActivity(intent);
            //            //}
            //            //else
            //            //{
            //            //    Toast.MakeText(this, "Invalid username or password", ToastLength.Long).Show();
            //            //}
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Toast.MakeText(this, "Error", ToastLength.Long).Show();
            //    }
            //    finally
            //    {
            //        sqlconn.Close();
            //    }
            //    #endregion
            //}

            ////private void HandleEvents()
            ////{
            ////    throw new NotImplementedException();
            ////}

            //private void FindViews()
            //{
            //    eventListView = FindViewById<ListView>(Resource.Id.eventListView);
            //    hotDogNameTextView = FindViewById<TextView>(Resource.Id.hotDogNameTextView);
            //    eventDateView = FindViewById<TextView>(Resource.Id.eventDateView);
            //}
        }
    }
}