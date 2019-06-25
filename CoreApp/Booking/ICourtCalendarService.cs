using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.BookingModel;

namespace CoreApp.Booking
{
    public interface ICourtCalendarService
    {
        // consider using DateTime.Min / DateTime.Max instead of null values for start/end parameters
        IEnumerable<CourtCalendarModel> GetFreeCalendars(int branchId, DateTime? start = null, DateTime? end = null);
        IEnumerable<NearestCourtModel> GetNearestAvailableCalendars(int branchId, DateTime start, DateTime end);
        IEnumerable<CourtCalendarModel> GetBranchCalendars(int branchId);
    }
}
