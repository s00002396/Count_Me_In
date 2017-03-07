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
    public class EventGroup
    {
        public int EventGroupId { get; set; }
        public string Title { get; set; }
        //public string ImagePath { get; set; }
        public List<Event> Events { get; set; }
    }
}