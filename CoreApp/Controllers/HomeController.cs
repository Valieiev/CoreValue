using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using CoreApp.Models;
using Google.Apis.Calendar.v3.Data;

namespace CoreApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        CalendarAPI api = new CalendarAPI();
        public Dictionary<string, string> courts = new Dictionary<string, string>();
        public HomeController(IMapper mapper)
        {
            _mapper = mapper;
            try
            {
                var calendars = api.GetCalendars();



                for (int i = 0; i < calendars.Count; i++)
                {
                    courts.Add(calendars[i].Id, calendars[i].Summary);
                }
            }
            catch{        }  ;

        }

        public IActionResult Index(string calendarid, string userinfo)
        {
            ViewBag.calendars = courts;
            IList<Event> events = new List<Event>();
            IList<EventCalendar> model = new List<EventCalendar>();
            if (!String.IsNullOrEmpty(calendarid))
            {
                events = api.GetEvents(calendarid);
                model = _mapper.Map<IList<EventCalendar>>(events);
            }

            if (!String.IsNullOrEmpty(userinfo))
            {
                events = api.GetEvents(calendarid).Where(c => c.Description.Contains(userinfo)).ToList();
                model = _mapper.Map<IList<EventCalendar>>(events);
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}
