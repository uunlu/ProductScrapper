using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductScrapper.Core.Common
{
    public class DatePeriod
    {
        public DatePeriod()
        {

        }
        public DatePeriod(DateTime? start, DateTime? end)
        {
            Start = start.Value;
            End = end.Value;
        }

        public TimeSpan Duration { get; }
        public DateTime? End { get; }
        public DateTime? Start { get; }

        // TODO : implement these methods
        //public static DatePeriod CreateForMonth(int month, int year);
        //public static DatePeriod CreateFromEndDate(DateTime end, TimeSpan? duration = default(TimeSpan?));
        //public static DatePeriod CreateFromStartDate(DateTime start, TimeSpan? duration = default(TimeSpan?));
        //public bool Contains(DateTime date);
        //public bool OverlapsWith(DatePeriod otherPeriod);
    }
}
