using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApp.BookingModel;

namespace CoreApp.Booking
{
    public interface ICourtCalendarService
    {
        IEnumerable<CourtCalendarModel> GetFreeCalendars(int branchId, DateTime? start = null, DateTime? end = null);
        IEnumerable<NearestCourtModel> GetNearestAvailableCalendars(int branchId, DateTime start, DateTime end);
        IEnumerable<CourtCalendarModel> GetBranchCalendars(int branchId);
    }
}
