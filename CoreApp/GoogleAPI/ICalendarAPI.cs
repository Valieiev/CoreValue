using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;

namespace CoreApp.GoogleAPI
{
    public interface ICalendarAPI
    {
        Task<IList<CalendarListEntry>> GetCalendars();

        Task<Calendar> GetCalendarById(string Id);

        void CreateCalendar(Calendar cal);

        void EditCalendar(string id, Calendar cal);

        void DeleteCalendar(string id);
        
        void CreateEvent(Event item, string calendarId);

        Task<IList<Event>> GetEvents(string calendarId);

        Task<Event> GetEventById(string calendarId, string eventId);

        void EditEvent(Event item, string calendarId, string eventId);

        void DeleteEvent(string calendarId, string eventId);
    } 
}
