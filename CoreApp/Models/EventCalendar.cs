using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [DisplayName("Client")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Date Start")]
        public EventDateTime Start { get; set; }
        [Required]
        [DisplayName("Date End")]
        [CompareDate("Start", ErrorMessage = "End date must be more than Start date")]
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

    
    public class CompareDateAttribute : CompareAttribute
    {
        public CompareDateAttribute(string otherProperty)
            : base(otherProperty) { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var validationitem = (EventDateTime) value;
                var property = validationitem.DateTime;
                var anothervalidationitem = (EventDateTime)validationContext.ObjectType.GetProperty(OtherProperty).GetValue(validationContext.ObjectInstance, null);
                var anotherProp = anothervalidationitem.DateTime;

                if (anotherProp < property)
                    return ValidationResult.Success;
            }
            catch (NullReferenceException)
            {
                ErrorMessage = "Convert Error";
            }

            return new ValidationResult(ErrorMessage);
        }
    }

}
