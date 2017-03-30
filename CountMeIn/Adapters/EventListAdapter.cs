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
using CountMeIn;

namespace ListViewEvents
{
    class MyListViewAdapter : BaseAdapter<Person>
    {
        private List<Person> mItems;
        private Context mContext;

        public MyListViewAdapter(Context context, List<Person> items)
        {
            mItems = items;
            mContext = context;
        }
        public override int Count
        {
            get { return mItems.Count; }
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override Person this[int position]
        {
            get { return mItems[position]; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.UpComingEventRowView, null, false);
            }
            TextView eventDate = row.FindViewById<TextView>(Resource.Id.eventDateView);
            eventDate.Text = mItems[position].EventDate;

            TextView groupName = row.FindViewById<TextView>(Resource.Id.groupNameView);
            groupName.Text = mItems[position].GroupName;

            TextView eventName = row.FindViewById<TextView>(Resource.Id.eventNameView);
            eventName.Text = mItems[position].EventName;

            TextView eventTime = row.FindViewById<TextView>(Resource.Id.timeView);
            eventTime.Text = mItems[position].Time;

            return row;
        }
    }
}