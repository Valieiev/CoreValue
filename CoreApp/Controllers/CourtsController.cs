using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreApp.Data;
using CoreApp.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Authorization;

namespace CoreApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class CourtsController : Controller
    {
        private readonly ApplicationDbContext _context;
        static string[] Scopes = { CalendarService.Scope.Calendar };
        public static UserCredential credential;
        private CalendarService calendarservice;
        public CourtsController(ApplicationDbContext context)
        {
            _context = context;
            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
            calendarservice = new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "Discovery Sample",
                ApiKey = "AIzaSyCR8ALHcc9MtSPz7tUKeANByRCu9qAz1gA",
            });




        }

        // GET: Courts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Court.ToListAsync());
        }

        // GET: Courts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var court = await _context.Court
                .FirstOrDefaultAsync(m => m.Id == id);
            if (court == null)
            {
                return NotFound();
            }

            return View(court);
        }

        // GET: Courts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Summary,Location,Description")] Court court)
        {
            if (ModelState.IsValid)
            {

                Calendar cal = new Calendar();
                cal.Summary = court.Summary;
                cal.Location = court.Location;
                cal.Description = court.Description;
                CalendarsResource.InsertRequest addCalendar = calendarservice.Calendars.Insert(cal);
                addCalendar.Execute();
                var calendars = calendarservice.CalendarList.List().Execute().Items;
                var createdCalendar = calendars.Where(c => c.Summary == court.Summary).FirstOrDefault();

                court.Id =  createdCalendar.Id;
                _context.Add(court);
                await _context.SaveChangesAsync();

                //Event newEvent = new Event()
                //{
                //    Summary = "Google I/O 2015",
                //    Location = "800 Howard St., San Francisco, CA 94103",
                //    Description = "A chance to hear more about Google's developer products.",
                //    Start = new EventDateTime()
                //    {
                //        DateTime = DateTime.Parse("2019-06-24T18:00:00-07:00"),
                //        TimeZone = "Europe/Kiev",
                //    },
                //    End = new EventDateTime()
                //    {
                //        DateTime = DateTime.Parse("2019-06-24T19:00:00-07:00"),
                //        TimeZone = "Europe/Kiev",
                //    },
                //    Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=2" },
                //    Attendees = new EventAttendee[] {
                //        new EventAttendee() { Email = "waynelaren@gmail.com" },
                //        new EventAttendee() { Email = "sbrin@example.com" },
                //    },
                //    Reminders = new Event.RemindersData()
                //    {
                //        UseDefault = false,
                //        Overrides = new EventReminder[] {
                //            new EventReminder() { Method = "email", Minutes = 24 * 60 },
                //            new EventReminder() { Method = "sms", Minutes = 10 },
                //        }
                //    }
                //};

                //EventsResource.InsertRequest request = calendarservice.Events.Insert(newEvent, createdCalendar.Id);
                //request.Execute();

                return RedirectToAction(nameof(Index));
            }
            return View(court);
        }

        // GET: Courts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var court = await _context.Court.FindAsync(id);
            
            if (court == null)
            {
                return NotFound();
            }
            return View(court);
        }

        // POST: Courts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Summary,Location,Description")] Court court)
        {
            if (id != court.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var calendars = calendarservice.CalendarList.List().Execute().Items;
                    Calendar update = new Calendar();
                    update.Summary = court.Summary;
                    update.Description = court.Description;
                    update.Location = court.Location;
                    update.Id = id;
                    calendarservice.Calendars.Patch(update, id).Execute();


                    _context.Update(court);
                    await _context.SaveChangesAsync();
                    

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!String.IsNullOrEmpty(court.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(court);
        }

        // GET: Courts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var court = await _context.Court
                .FirstOrDefaultAsync(m => m.Id == id);
            if (court == null)
            {
                return NotFound();
            }

            return View(court);
        }

        // POST: Courts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var court = await _context.Court.FindAsync(id);
            _context.Court.Remove(court);
            await _context.SaveChangesAsync();
            var calendars = calendarservice.CalendarList.List().Execute().Items;
            var deletedcalendar = calendars.Where(c => c.Id == court.Id).FirstOrDefault();
            calendarservice.CalendarList.Delete(deletedcalendar.Id).Execute();
            return RedirectToAction(nameof(Index));
        }

        private bool CourtExists(string id)
        {
            return _context.Court.Any(e => e.Id == id);
        }
    }
}
