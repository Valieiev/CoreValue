using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreApp.GoogleAPI;
using Microsoft.AspNetCore.Mvc;
using CoreApp.Models;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;

namespace CoreApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICalendarAPI _api;
        public Dictionary<string, string> courts = new Dictionary<string, string>();

    

        public HomeController(IMapper mapper, ICalendarAPI api)
        {
            _mapper = mapper;
            _api = api;

            var calendars = _api.GetCalendars();

                for (int i = 0; i < calendars.Result.Count; i++)
                {
                    courts.Add(calendars.Result[i].Id, calendars.Result[i].Summary);
                }

        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ShowBookings(string calendarid, string userinfo)
        {
            ViewBag.calendars = courts;
            IList<Event> events = new List<Event>();
            IList<EventCalendar> model = new List<EventCalendar>();
            if (calendarid == "- Please select court -" || userinfo == "- Please enter your phonenumber/surname -")
            {
                return View(model);
            }

            if (!String.IsNullOrEmpty(calendarid))
            {
                events = await _api.GetEvents(calendarid);
                model = _mapper.Map<IList<EventCalendar>>(events);
            }

            if (!String.IsNullOrEmpty(userinfo))
            {
                events =  _api.GetEvents(calendarid).Result.Where(c => c.Description.Contains(userinfo)).ToList();
                model = _mapper.Map<IList<EventCalendar>>(events);
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}
