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

namespace CountMeIn.Model
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventDate { get; set; }
        public string EventName { get; set; }
        public string GroupName { get; set; }
        public string Time { get; set; }

    }
    class Person
    {
        public string EventDate { get; set; }
        public string EventName { get; set; }
        public string GroupName { get; set; }
        public string Time { get; set; }
    }
    public static class Globals
    {
        public static int s_Name = 0; // Modifiable in Code
        //public const int; // VALUE = 10; // Unmodifiable
    }
}