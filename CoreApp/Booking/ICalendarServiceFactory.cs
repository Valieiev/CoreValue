using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3;

namespace CoreApp.Booking
{
    public interface ICalendarServiceFactory
    {
        CalendarService CreateCalendarService();
    }
}
