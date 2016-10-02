using Newtonsoft.Json.Linq;
using ProductScrapper.Core.Models;
using ScrapeEngine;
using ScrapeEngine.Root;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProductScrapper.ScrapperEngine.Dickssportinggoods
{
    public class ProductDetailsManager:BaseClass
    {
        private string productUrl { get; set; }
        private string html { get; set; }
        private HtmlAgilityPack.HtmlDocument doc { get; set; }
        private Product product { get; set; }
        private ProductDetailsManager()
        {

        }
        public ProductDetailsManager(string url)
        {
            productUrl = url;
            product = new Product();
        }

        public void GetProductDetails()
        {
            html = Browser.HttpWebRequestGet(productUrl);
            doc = Helper.GetDocument(html);

            GetTitle();
            GetPrice();
            GetAvailableSizes();
            GetProductInformation();
            GetProductFeatures();
            GetMainImage();
            GetOtherImages();

            SaveLineTabDelimited(product);
        }

        public void GetTitle()
        {
            var title = Helper.GetSingleNodeByClass(doc, "h1", "prod-title")?.InnerText;

            product.Title = title;
        }

        public void GetPrice()
        {
            var price = Helper.GetSingleNode(doc, "//span[@itemprop='price']")?.InnerText
                .Replace("&#036;", "$")
                .Replace("NOW:", string.Empty).Trim();

            product.Price = price;
        }

        public void GetAvailableSizes()
        {
            Regex jsonRegex = new Regex("productJson.*?(?=</script>)", RegexOptions.Singleline);
            var json = jsonRegex.Match(html).Value
            .Replace("productJson", string.Empty)
            .Replace("=", string.Empty)
            .Replace("&#039;", "\"")
            .Replace(";", string.Empty);
            dynamic serializer = JObject.Parse(json);

            foreach (var item in serializer.skus)
            {
                if (item.avail == "IN_STOCK")
                    product.AvailableSizes.Add(item.size.ToString());
            }
        }

        public void GetProductInformation()
        {
            var productInformation = Helper.GetSingleNodeByClass(doc, "div", "prod-short-desc")?.InnerText.Trim();

            product.ProductInformation = productInformation;
        }

        public void GetProductFeatures()
        {
            var features = Helper.GetCollectionByClass(doc, "div", "prod-long-desc", "ul/li");

            if (features == null) return;

            foreach (var item in features)
            {
                var inner = item?.InnerText;
                if (!string.IsNullOrEmpty(inner))
                    product.ProductFeatures.Add(inner.Trim());
            }
        }

        public void GetMainImage()
        {
            Regex jsonRegex = new Regex("productJson.*?(?=</script>)", RegexOptions.Singleline);
            var json = jsonRegex.Match(html).Value
            .Replace("productJson", string.Empty)
            .Replace("=", string.Empty)
            .Replace("&#039;", "\"")
            .Replace(";", string.Empty);
            dynamic serializer = JObject.Parse(json);

            var largerEnhancedImageURL = serializer.alternateViews?.main?.largerEnhancedImageURL.ToString().Trim();

            if (largerEnhancedImageURL == null) return;

            var thumbnailImageURL = serializer.alternateViews?.main?.thumbnailImageURL.ToString().Trim();
            var mainImageURL = serializer.alternateViews?.main?.mainImageURL.ToString().Trim();
            var enhancedImageURL = serializer.alternateViews?.main?.enhancedImageURL.ToString().Trim();

            product.MainImage.LargerEnhancedImageURL = largerEnhancedImageURL;
            product.MainImage.ThumbnailImageURL = thumbnailImageURL;
            product.MainImage.MainImageURL = mainImageURL;
            product.MainImage.EnhancedImageURL = enhancedImageURL;
        }

        public void GetOtherImages()
        {
            Regex jsonRegex = new Regex("productJson.*?(?=</script>)", RegexOptions.Singleline);
            var json = jsonRegex.Match(html).Value
            .Replace("productJson", string.Empty)
            .Replace("=", string.Empty)
            .Replace("&#039;", "\"")
            .Replace(";", string.Empty);
            dynamic serializer = JObject.Parse(json);
            // alternateViews

            int index = 1;
            while (true)
            {
                var alt = "alternate" + index;
                var largerEnhancedImageURL = serializer.alternateViews[alt]?.largerEnhancedImageURL.ToString();
                if (largerEnhancedImageURL == null) break;

                var thumbnailImageURL = serializer.alternateViews[alt]?.thumbnailImageURL.ToString();
                var mainImageURL = serializer.alternateViews[alt]?.mainImageURL.ToString();
                var enhancedImageURL = serializer.alternateViews[alt]?.enhancedImageURL.ToString();

                product.ProductImages.Add(new ProductImage
                {
                    LargerEnhancedImageURL = largerEnhancedImageURL,
                    ThumbnailImageURL = thumbnailImageURL,
                    MainImageURL = mainImageURL,
                    EnhancedImageURL = enhancedImageURL
                });

                index++;
            }
        }


    }
}
