using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;

namespace CoreApp.Models
{
    public class EventCalendar : Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string calendarId { get; set; }
        public string Summary { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public EventDateTime Start { get; set; }
        public EventDateTime End { get; set; }
        public EventAttendee[] Attendees { get; set; }
        public EventReminder Reminder { get; set; }

        public EventCalendar()
        {
            Start = new EventDateTime();
            End = new EventDateTime();
        }
    }
    
}
