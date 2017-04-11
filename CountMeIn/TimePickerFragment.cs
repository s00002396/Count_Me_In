using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace CountMeIn
{
    public class TimePickerFragment : DialogFragment, TimePickerDialog.IOnTimeSetListener
    {
        public static readonly string TAG = "X:" + typeof(TimePickerFragment).Name.ToUpper();
        
        Action<string> _dateSelectedHandler = delegate { };

        public static TimePickerFragment NewInstance(Action<string> OnTimeSet)
        {
            TimePickerFragment frag = new TimePickerFragment();
            frag._dateSelectedHandler = OnTimeSet;

            return frag;
        }
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime currently = DateTime.Now;
            TimePickerDialog dialog = new TimePickerDialog(Activity, this, currently.Hour, currently.Minute, false);
            return dialog;
        }
        public void OnTimeSet(TimePicker view, int hourOfDay, int minute)
        {
            string time = string.Format("{0}:{1}", hourOfDay, minute.ToString().PadLeft(2, '0'));
            _dateSelectedHandler(time);
        }
    }
}