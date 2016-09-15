using ProductScrapper.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductScrapper.AppServices.Dtos
{
    public class DatePeriodDto
    {
        internal static DatePeriodDto CreateFrom(DatePeriod value)
        {
            return new DatePeriodDto
            {
                Start = value.Start,
                End = value.End
            };
        }

        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
