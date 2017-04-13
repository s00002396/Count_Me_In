using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System.Data.SqlClient;
using CountMeIn.Model;
using Android.Content.PM;

namespace CountMeIn
{
    [Activity(Label = "Create Venue", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class CreateGroupActivity : Activity
    {
        private Button btnCreateGroup;
        private EditText username;
        private EditText pword;
        private EditText textVenueName;
        private EditText textVenuAddress;
        private EditText textVenuPhone;

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
            textVenueName = FindViewById<EditText>(Resource.Id.textVenueName);
            textVenuAddress = FindViewById<EditText>(Resource.Id.textVenuAddress);
            textVenuPhone = FindViewById<EditText>(Resource.Id.textVenuPhone);
        }
        private void HandleEvents()
        {
            btnCreateGroup.Click += BtnCreateGroup_Click;
        }
        private void BtnCreateGroup_Click(object sender, EventArgs e)
        {
            Globals.sqlconn = new System.Data.SqlClient.SqlConnection(Globals.connsqlstring);
            try
            {
                if (textVenueName.Text != "" && textVenuAddress.Text != "")
                {
                    Globals.sqlconn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO Venue_Table(Venue_Name,Venue_PhoneNo,Venue_Address,Venue_Eircode) VALUES(@venueName,@phoneNo, @address,@eircode)", Globals.sqlconn);

                    cmd.Parameters.AddWithValue("@venueName", textVenueName.Text);
                    cmd.Parameters.AddWithValue("@phoneNo", textVenuPhone.Text);
                    cmd.Parameters.AddWithValue("@address", textVenuAddress.Text);
                    cmd.Parameters.AddWithValue("@eircode", "");
                    cmd.ExecuteNonQuery();
                    Toast.MakeText(this, "Venue Created", ToastLength.Long).Show();
                    var intent = new Intent(this, typeof(LoginActivity));
                    StartActivity(intent);
                }
                else
                {
                    if (textVenueName.Text == "")
                    {
                        Toast.MakeText(this, "Enter venue name", ToastLength.Long).Show();
                    }
                    else if (textVenuAddress.Text == "")
                    {
                        Toast.MakeText(this, "Enter venue address", ToastLength.Long).Show();
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
            }
        }
    }
}