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

namespace CountMeIn.Adapters
{
    class GuestInviteAdapter : BaseAdapter<Member>
    {
        private List<Member> mItems;
        private Context mContext;

        public GuestInviteAdapter(Context context, List<Member> items)
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
        public override Member this[int position]
        {
            get { return mItems[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.GuestInviteRow, null, false);
            }
            TextView eventDate = row.FindViewById<TextView>(Resource.Id.eventDateView);
            eventDate.Text = mItems[position].Member_Name;

            TextView groupName = row.FindViewById<TextView>(Resource.Id.groupNameView);
            groupName.Text = mItems[position].GroupName;

            TextView eventName = row.FindViewById<TextView>(Resource.Id.eventNameView);
            eventName.Text = mItems[position].Member_Phone;

            TextView eventTime = row.FindViewById<TextView>(Resource.Id.timeView);
            eventTime.Text = mItems[position].UserName;

            return row;
        }
    }

    class GuestInviteAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}