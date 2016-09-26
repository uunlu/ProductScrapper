using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductScrapper.Core.Models
{
    public class Product:AggregateRoot
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string Rating { get; set; }
        public string Url { get; set; }
    }
}
