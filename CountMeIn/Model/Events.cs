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
    public class Person
    {
        public int EventId { get; set; }
        public string EventDate { get; set; }
        public string EventName { get; set; }
        public string GroupName { get; set; }
        public string Time { get; set; }
    }
    public class Member
    {
        public int Member_Id { get; set; }
        public string Member_Name { get; set; }
        public string Member_Phone { get; set; }
        public string UserName { get; set; }
        public string GroupName { get; set; }

    }
    public static class Globals
    {
        public static string myPhoneNumber;
        public static SqlConnection sqlconn;
        public static string connsqlstring = string.Format("Server=tcp:dominicbrennan.database.windows.net,1433;Initial Catalog=CountMeIn;Persist Security Info=False;User ID=dominicbrennan;Password=Fld118yi;MultipleActiveResultSets=False;Trusted_Connection=false;Encrypt=false;Connection Timeout=30;");
    }
}