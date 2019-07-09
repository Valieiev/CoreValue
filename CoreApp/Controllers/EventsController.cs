using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreApp.Data;
using CoreApp.GoogleAPI;
using CoreApp.Models;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreApp.Controllers
{
    [Authorize(Roles = "manager")]
    public class EventsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        private readonly ICalendarAPI _api;
        private static string _calendarId;

        public EventsController(IMapper mapper, ApplicationDbContext context, ICalendarAPI api)
        {
            _mapper = mapper;
            _context = context;
            _api  = api;
        }
        // GET: Events
        public async Task<IActionResult> Index(string id)
        {
            
            if (String.IsNullOrEmpty(id)) id = _calendarId;
            else _calendarId = id;
                var list = await  _api.GetEvents(id);

                var model = _mapper.Map<IList<EventCalendar>>(list);
            ViewData["Title"] =  _api.GetCalendarById(id).Result.Summary;
                return View(model);

        }

        // GET: Events/Details/5
        public ActionResult Details(int id)
        {
            return null;
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            List<Client> clients = _context.Clients.ToList();
            ViewBag.Clients = clients.Select(s => new SelectListItem
                {
                Text = $"{s.Name} {s.Surname} : {s.PhoneNumber}",
                Value = $"{s.Name} {s.Surname} : {s.PhoneNumber}"
            }
            ).ToList();
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventCalendar item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var calendarevent = _mapper.Map<Event>(item);
                    _api.CreateEvent(calendarevent, _calendarId);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {

                    return null;
                }
            }
            else

            {
                List<Client> clients = _context.Clients.ToList();
                ViewBag.Clients = clients.Select(s => new SelectListItem
                    {
                        Text = $"{s.Name} {s.Surname} : {s.PhoneNumber}",
                        Value = $"{s.Name} {s.Surname} : {s.PhoneNumber}"
                    }
                ).ToList();
                return View(item);
            }
            
        }

        // GET: Events/Delete/5
        public ActionResult DeleteShow(string id)
        {
            var item  = _api.GetEventById(_calendarId,id);
            var model = _mapper.Map<EventCalendar>(item);
            return View(model);
        }

        // POST: Events/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            try
            {
                _api.DeleteEvent(_calendarId,id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return null;
            }
        }
    }
}