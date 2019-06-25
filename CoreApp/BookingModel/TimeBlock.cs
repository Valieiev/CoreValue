using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.BookingModel
{
    [DebuggerDisplay("Start = {Start}, End = {End}")]
    public class TimeBlock
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public static DateTime NormalizeDateTime(DateTime original, DateTime timeBlock)
        {
            var dateTime = original.Date;
            int hour;
            int minute;
            if (timeBlock.Hour < original.Hour)
            {
                hour = original.Hour;
                minute = original.Minute;
            }
            else if (timeBlock.Hour == original.Hour)
            {
                hour = original.Hour;
                minute = timeBlock.Minute > original.Minute ? timeBlock.Minute : original.Minute;
            }
            else
            {
                hour = timeBlock.Hour;
                minute = timeBlock.Minute;
            }
            dateTime = dateTime.AddHours(hour);
            dateTime = dateTime.AddMinutes(minute);
            return dateTime;
        }

        public static DateTime? NormalizeDate(DateTime? original, DateTime? timeBlock)
        {
            if (!timeBlock.HasValue)
                return null;
            return original.GetValueOrDefault().Date.AddHours(timeBlock.Value.Hour).AddMinutes(timeBlock.Value.Minute);
        }
    }
    public class TimeBlockComparer : IEqualityComparer<TimeBlock>
    {
        public bool Equals(TimeBlock x, TimeBlock y)
        {
            return x.Start == y.Start && x.End == y.End;
        }

        public int GetHashCode(TimeBlock obj)
        {
            return obj.Start.GetHashCode() ^ obj.End.GetHashCode();
        }
    }

    public static class TimeBlockExtensions
    {
        public static IEnumerable<TimeBlock> MergeTimeBlocks(this IOrderedEnumerable<TimeBlock> origin)
        {
            var result = new List<TimeBlock>();
            TimeBlock unit = null;
            foreach (var timeBlock in origin)
            {
                if (unit != null && timeBlock.Start <= unit.End)
                {
                    if (timeBlock.End > unit.End)
                    {
                        unit.End = timeBlock.End;
                    }
                }
                else
                {
                    unit = timeBlock;
                    result.Add(unit);
                }
            }
            return result;
        }
    }
}
