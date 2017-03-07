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
using CountMeIn.Service;
using CountMeIn.Adapters;

namespace CountMeIn
{
    [Activity(Label = "UpComing Event", MainLauncher = false)]
    public class UpComingEventActivity : Activity
    {
        private ListView eventListView;
        private List<Event> allEvents;
        private EventDataService eventDataService;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.UpComingEvent);

            eventListView = FindViewById<ListView>(Resource.Id.eventListView);

            eventDataService = new EventDataService();

            allEvents = eventDataService.GetAllEvents();

            eventListView.Adapter = new EventListAdapter(this, allEvents);
        }
    }
}