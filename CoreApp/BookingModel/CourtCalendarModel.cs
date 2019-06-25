using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.BookingModel
{
    [DebuggerDisplay("CalendarName={CalendarName},GoogleCalendarId={GoogleCalendarId}")]
    public class CourtCalendarModel : BookingModelBase
    {
        #region Fields And Properties	

        public string GoogleCalendarId { get; set; }

        public string CalendarName { get; set; }

        #endregion
    }
}
