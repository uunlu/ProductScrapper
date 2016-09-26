using ProductScrapper.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProductScrapper.Core.Common.Website;

namespace ProductScrapper.Core.Models
{
    public class Message:AggregateRoot
    {
        public string UrlToScrap { get; set; }
        public Websites Site { get; set; }
        public string ProductType { get; set; }
    }
}
