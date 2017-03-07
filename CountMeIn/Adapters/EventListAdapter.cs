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
    public class EventListAdapter : BaseAdapter<Event>
    {
        List<Event> items;
        Activity context;

        public EventListAdapter(Activity context, List<Event> items):base()
        {
            this.context = context;
            this.items = items;
        }

        public override Event this[int position]
        {
            get
            {
                return items[position];
            }
        }

        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1,null);
            }
            convertView.FindViewById<TextView>(Android.Resource.Id.Text1).Text = item.EventName;
            return convertView;
        }
    }
}