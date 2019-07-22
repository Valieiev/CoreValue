using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3.Data;

namespace CoreApp.Models
{
    public class EventCalendar
    {
       
        public  string Id { get; set; }

        [Required]
        [RegularExpression(@"[а-яА-Яa-zA-Z0-9\s]{2,50}", ErrorMessage = "Only letters or numbers allowed")]
        public string Summary { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public EventDateTime Start { get; set; }
        [Required]
        public EventDateTime End { get; set; }

        public EventAttendee[] Attendees { get; set; }
        public Event.RemindersData Reminders { get; set; }
        public string Transparency { get; set; }

        public EventCalendar()
        {
            Start = new EventDateTime() {TimeZone = "Europe/Kiev"};
            End = new EventDateTime() { TimeZone = "Europe/Kiev" };
            Attendees = new EventAttendee[] {new EventAttendee() {Email = "corecourtbooking@gmail.com" } };
            Reminders = new Event.RemindersData()
            {
                UseDefault = false,
                Overrides = new EventReminder[]
                {
                    new EventReminder() {Method = "email", Minutes = 24 * 60},
                    new EventReminder() {Method = "sms", Minutes = 10},
                }
            };
            Transparency = "opaque";
        }
    }
    
}
