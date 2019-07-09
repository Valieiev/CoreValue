using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreApp.Data;
using CoreApp.GoogleAPI;
using CoreApp.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters.Internal;

namespace CoreApp.Controllers
{
    
    public class CourtsController : Controller
    {

        private readonly IMapper _mapper;

        private readonly ICalendarAPI _api;

        public CourtsController( IMapper mapper, ICalendarAPI api)
        {
            _mapper = mapper;
            _api = api;
        }

        // GET: Courts
        public async Task<IActionResult> Index()
        {
            var list = await _api.GetCalendars();
            var model =  _mapper.Map<IList<Court>>(list);
             return View( model);
        }

        // GET: Courts/Details/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var court = await _api.GetCalendarById(id);
            var item = _mapper.Map<Court>(court);
            if (court == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Courts/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Summary,Location,Description")] Court court)
        {
            if (ModelState.IsValid)
            {

                Calendar cal = _mapper.Map<Calendar>(court);
                 _api.CreateCalendar(cal);

                return RedirectToAction(nameof(Index));
            }
            return View(court);
        }

        // GET: Courts/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var court = await _api.GetCalendarById(id);
            var item = _mapper.Map<Court>(court);

            if (court == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Courts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(string id, [Bind("Id,Summary,Location,Description")] Court court)
        {
            if (id != court.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {                    
                    Calendar update = _mapper.Map<Calendar>(court);
                     _api.EditCalendar(id,update);
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var court = await _api.GetCalendarById(id);
            var item = _mapper.Map<Court>(court);
            if (court == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Courts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            _api.DeleteCalendar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
