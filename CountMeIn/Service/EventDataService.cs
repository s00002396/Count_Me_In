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
using CountMeIn.Repository;
using CountMeIn.Model;

namespace CountMeIn.Service
{
    public class EventDataService
    {
        private static EventRepository eventRepository = new EventRepository();

        public List<Event> GetAllEvents()
        {
            return eventRepository.GetAllEvents();
        }

        public List<EventGroup> GetGroupedEvents()
        {
            return eventRepository.GetGroupedEvents();
        }

        public List<Event> GetEventsForGroup(int eventGroupId)
        {
            return eventRepository.GetEventsForGroup(eventGroupId);
        }

        public Event GetEventById(int eventId)
        {
            return eventRepository.GetEventById(eventId);
        }
    }
}