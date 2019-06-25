using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.BookingModel 
{
    public class NearestCourtModel : CourtCalendarModel
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
