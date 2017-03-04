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
    [Activity(Label = "MyDateActivity")]
    public class MyDateActivity : Activity
    {
        public Button button;
        private EditText date;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MyDate);

            //button = FindViewById<Button>(Resource.Id.MyButton);
            date = FindViewById<EditText>(Resource.Id.MyEditButton);

            //button.Click += Button_Click;
            date.Click += Date_Click;
            //DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            //{
            //    button.Text = time.ToLongDateString();
            //});
            //frag.Show(FragmentManager, DatePickerFragment.TAG);

        }

        private void Date_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                date.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        //private void Button_Click(object sender, EventArgs e)
        //{
        //    DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
        //    {
        //        button.Text = time.ToLongDateString();
        //    });
        //    frag.Show(FragmentManager, DatePickerFragment.TAG);
        //}

        //private void Button_Click(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}