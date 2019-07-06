using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using CoreApp.Models;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;

namespace CoreApp
{


    public class CalendarAPI
    {
        public IList<CalendarListEntry> GetCalendars()
        {
            var items = Startup.calendarservice.CalendarList.List().Execute().Items;
            items.RemoveAt(0);
            return (items);
        }

        public Calendar GetCalendarById(string Id)
        {
            return (Startup.calendarservice.Calendars.Get(Id).Execute());
        }

        public void CreateCalendar( Calendar cal)
        {
            CalendarsResource.InsertRequest addCalendar = Startup.calendarservice.Calendars.Insert(cal);
            addCalendar.Execute();
        }

        public void EditCalendar(string id, Calendar cal)
        {
            Startup.calendarservice.Calendars.Patch(cal, id).Execute();
        }

        public void DeleteCalendar(string id)
        {
            Startup.calendarservice.Calendars.Delete(id).Execute();
        }

        public void CreateEvent(Event item,string calendarId)
        {
            Startup.calendarservice.Events.Insert(item, calendarId).Execute();
        }

        public IList<Event> GetEvents( string calendarId)
        {
            return Startup.calendarservice.Events.List(calendarId).Execute().Items;
        }
        public Event GetEventById(string calendarId, string eventId)
        {
            return Startup.calendarservice.Events.Get(calendarId, eventId).Execute();
        }

        public void EditEvent(Event item,string calendarId, string eventId)
        {
            Startup.calendarservice.Events.Update(item, calendarId, eventId).Execute();
        }
        public void DeleteEvent( string calendarId, string eventId)
        {
            Startup.calendarservice.Events.Delete( calendarId, eventId).Execute();
        }



    }
}
