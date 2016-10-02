using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductScrapper.Core.Models
{
    public class Product:AggregateRoot
    {
        public string Title { get; set; }
        public string Price { get; set; }
        public List<string> AvailableSizes { get; set; } = new List<string>();
        public List<string> ProductFeatures { get; set; } = new List<string>();
        public string ProductInformation { get; set; }
        public string Rating { get; set; }
        public ProductImage MainImage { get; set; } = new ProductImage();
        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public string Url { get; set; }
    }

    public class ProductImage
    {
        public string ThumbnailImageURL { get; set; }
        public string MainImageURL { get; set; }
        public string EnhancedImageURL { get; set; }
        public string LargerEnhancedImageURL { get; set; }
    }
}
