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
using System.Data.SqlClient;
using System.Data;

namespace CountMeIn.Repository
{
    public class EventRepository
    {
        private static List<EventGroup> eventGroups = new List<EventGroup>();

        #region HardCoded
        //    {  
        //      new EventGroup()
        //      {
        //        EventGroupId = 1, Title = "Poker Nite", Events = new List<Event>()
        //        {
        //            new Event()
        //            {
        //                EventId=1,
        //                EventName = "Work Party",
        //                EventDate="25/03/17",
        //                EventTime="19:00",
        //                EventLocation="The Glasshouse"

        //            },
        //            new Event()
        //            {
        //                EventId=2,
        //                EventName = "Family Dinner",
        //                EventDate="17/03/17",
        //                EventTime="16:30",
        //                EventLocation="Yeats Tavern"
        //            },
        //            new Event()
        //            {
        //                EventId=3,
        //                EventName = "Poker Nite!",
        //                EventDate="02/04/17",
        //                EventTime="20:00",
        //                EventLocation="Bills House"
        //            }
        //        }
        //    },
        //        new EventGroup()
        //    {
        //        EventGroupId = 2, Title = "Work Outing", Events = new List<Event>() {
        //            new Event()
        //            {
        //                EventId=4,
        //                EventName = "Movie Night",
        //                EventDate="10/04/17",
        //                EventTime="21:00",
        //                EventLocation="Omniplex"
        //            },
        //            new Event()
        //            {
        //                EventId=5,
        //                EventName = "School Reunion",
        //                EventDate="20/05/17",
        //                EventTime="20:50",
        //                EventLocation="The Hall"
        //            },
        //            new Event()
        //            {
        //                EventId=6,
        //                EventName = "Going away party",
        //                EventDate="21/03/17",
        //                EventTime="17:30",
        //                EventLocation="Laura's House"
        //            },
        //            new Event()
        //            {
        //                EventId=7,
        //                EventName = "Graduation",
        //                EventDate="25/05/17",
        //                EventTime="17:30",
        //                EventLocation="The Pub?"
        //            }
        //        }
        //    }
        //};
        #endregion

        public List<Event> GetAllEvents()
        {
            IEnumerable<Event> events =
                from eventGroup in eventGroups
                from evt in eventGroup.Events
                select evt;
            return events.ToList<Event>();
        }
        public Event GetEventById(int eventId)
        {
            IEnumerable<Event> events =
                from eventGroup in eventGroups
                from evt in eventGroup.Events
                where evt.EventId == eventId
                select evt;
            return events.FirstOrDefault();
        }
        public List<EventGroup> GetGroupedEvents()
        {
            return eventGroups;
        }
        public List<Event> GetEventsForGroup(int eventGroupId)
        {
            var group = eventGroups.Where(h => h.EventGroupId == eventGroupId).FirstOrDefault();
            if (group != null)
            {
                return group.Events;
            }
            return null;
        }
    }
}