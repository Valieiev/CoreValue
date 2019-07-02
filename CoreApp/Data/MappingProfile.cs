using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreApp.Models;
using Google.Apis.Calendar.v3.Data;

namespace CoreApp.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CalendarListEntry, Court>();
            CreateMap<Court, CalendarListEntry>();

            CreateMap<Calendar, Court>();
            CreateMap<Court, Calendar>();

            CreateMap<EventCalendar, Event>();
            CreateMap<Event, EventCalendar>();
        }
    }
}
