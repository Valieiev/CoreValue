using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using CoreApp.GoogleAPI;
using CoreApp.Models;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;

namespace CoreApp
{


    public class CalendarAPI : ICalendarAPI
    {
        private readonly CalendarService _calendarService;

        public CalendarAPI(CalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        public async Task<IList<CalendarListEntry>> GetCalendars()
        {
            var items = await _calendarService.CalendarList.List().ExecuteAsync();
            //removing element [0] because first element it is standard calendar of your mail
            items.Items.RemoveAt(0);
            return (items.Items);
        }

        public async Task<Calendar> GetCalendarById(string Id)
        {
            return await  (_calendarService.Calendars.Get(Id).ExecuteAsync());
        }

        public void CreateCalendar( Calendar cal)
        {
            CalendarsResource.InsertRequest addCalendar = _calendarService.Calendars.Insert(cal);
             addCalendar.Execute();
        }

        public async void EditCalendar(string id, Calendar cal)
        {
            await _calendarService.Calendars.Patch(cal, id).ExecuteAsync();
        }

        public async void DeleteCalendar(string id)
        {
            await _calendarService.Calendars.Delete(id).ExecuteAsync();
        }

        public  void CreateEvent(Event item,string calendarId)
        {
             _calendarService.Events.Insert(item, calendarId).Execute();
        }

        public async Task<IList<Event>> GetEvents( string calendarId)
        {
           
            var items = await _calendarService.Events.List(calendarId).ExecuteAsync();
            return (items.Items) ;
        }
        public async Task<Event> GetEventById(string calendarId, string eventId)
        {
            return await _calendarService.Events.Get(calendarId, eventId).ExecuteAsync();
        }

        public async void EditEvent(Event item,string calendarId, string eventId)
        {
            await _calendarService.Events.Update(item, calendarId, eventId).ExecuteAsync();
        }
        public async void DeleteEvent( string calendarId, string eventId)
        {
            await _calendarService.Events.Delete( calendarId, eventId).ExecuteAsync();
        }



    }
}
