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

namespace CountMeIn.Repository
{
    public class EventRepository
    {
        private static List<EventGroup> eventGroups = new List<EventGroup>()
        {  
          new EventGroup()
          {
            EventGroupId = 1, Title = "Poker Nite", Events = new List<Event>()
            {
                new Event()
                {
                    EventId=1,
                    EventName = "Work Party",
                    EventDate="17/03/17",
                    EventTime="20:00",
                    EventLocation="Bills House"

                },
                new Event()
                {
                    EventId=2,
                    EventName = "Family Dinner",
                    EventDate="17/03/17",
                    EventTime="20:00",
                    EventLocation="Bills House"
                },
                new Event()
                {
                    EventId=3,
                    EventName = "Poker Nite!",
                    EventDate="17/03/17",
                    EventTime="20:00",
                    EventLocation="Bills House"
                }
            }
        },
            new EventGroup()
        {
            EventGroupId = 2, Title = "Work Outing", Events = new List<Event>() {
                new Event()
                {
                    EventId=4,
                    EventName = "Movie Night",
                   EventDate="17/03/17",
                    EventTime="20:00",
                    EventLocation="Bills House"
                },
                new Event()
                {
                    EventId=5,
                    EventName = "School Reunion",
                   EventDate="17/03/17",
                    EventTime="20:00",
                    EventLocation="Bills House"
                },
                new Event()
                {
                    EventId=6,
                    EventName = "Going away party",
                    EventDate="17/03/17",
                    EventTime="20:00",
                    EventLocation="Bills House"
                }
            }
        }
    };
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
        //public List<Event> GetFavoriteHotDogs()
        //{
        //    IEnumerable<Event> hotDogs =
        //        from eventGroup in eventGroups
        //        from evt in eventGroup.Events
        //        where evt.IsFavorite
        //        select evt;
        //    return hotDogs.ToList<Event>();
        //}
    }
}