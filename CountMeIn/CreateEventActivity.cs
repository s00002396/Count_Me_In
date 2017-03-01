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
    [Activity(Label = "Create an Event", MainLauncher = true)]
    public class CreateEventActivity : Activity
    {
        private DatePicker datePicker;
        private Button btnChange;
        private TextView txtDate;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CreateEvent);

            datePicker = FindViewById<DatePicker>(Resource.Id.datePicker);
            //btnChange = FindViewById<Button>(Resource.Id.change_date_button);
            //txtDate = FindViewById<TextView>(Resource.Id.txtViewDate);
            
            //txtDate.Text = (getDate());
            //btnChange.Click += BtnChange_Click;

        }

        //private void BtnChange_Click(object sender, EventArgs e)
        //{
        //    txtDate.Text = getDate();
        //}

        //private string getDate()
        //{
        //    StringBuilder strCurrentDate = new StringBuilder();
        //    int month = datePicker.Month + 1;
        //    strCurrentDate.Append("Date : " + month + "/" + datePicker.DayOfMonth + "/" + datePicker.Year);
        //    return strCurrentDate.ToString();
        //}
    }
}