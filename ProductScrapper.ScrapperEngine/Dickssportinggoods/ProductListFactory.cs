using ProductScrapper.Core.Models;
using ScrapeEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProductScrapper.ScrapperEngine.Dickssportinggoods
{
    public class ProductListFactory
    {
        private string productLinkUrl { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
        private string html { get; set; }
        private HtmlAgilityPack.HtmlDocument doc { get; set; }

        private ProductListFactory()
        {

        }

        public ProductListFactory(string url)
        {
            productLinkUrl = ChangeUrl(url);
            html = Browser.HttpWebRequestGet(url);
            doc = Helper.GetDocument(html);
        }

        private string ChangeUrl(string productLinkUrl)
        {
            Regex itemPerPageRegex = new Regex("ppp=\\d{2,3}");
            if (productLinkUrl.Contains("&ppp"))
            {
                var pp = itemPerPageRegex.Match(productLinkUrl).Value;
                productLinkUrl = productLinkUrl.Replace(pp, "ppp=144");
            }
            else
            {
                productLinkUrl += "&ppp=144";
            }
            return productLinkUrl;
        }

        public async Task<List<Message>> GetProductLinks()
        {

            int total = ExtractTotalNumberOfItems();
            int numberOfPages = ExtractTotalNumberOfPages();
            string page = "&page=";

            for (int i = 1; i <= numberOfPages; i++)
            {
                var url = productLinkUrl + page + i;
                var html = await Browser.HttpWebRequestGetAsync(url);
                doc = Helper.GetDocument(html);
                GetProductUrls();
                //break;
            }

            return Messages;
        }

        private void GetProductUrls()
        {
            var nodes = Helper.GetCollection(doc, "//li[contains(@class,'prod-item')]");

            if (nodes == null) return;

            foreach (var item in nodes)
            {
                var productUrl = item.SelectSingleNode(".//h2[@class='prod-title']/a").Attributes["href"].Value;
                var message = new Message
                {
                    UrlToScrap = productUrl,
                    Site = Core.Common.Website.Websites.Dickssportinggoods,
                     ProductType = "Shoe"
                };

                Messages.Add(message);
            }
        }

        private int ExtractTotalNumberOfItems()
        {
            var productCount = Helper.GetSingleNodeByClass(doc, "span", "product-count").InnerText
              .Replace("Products", string.Empty).Trim();

            int total = 0;

            int.TryParse(productCount, out total);

            return total;
        }


        private int ExtractTotalNumberOfPages()
        {
            var aTags = Helper.GetCollectionByClass(doc, "span", "pages", "a");
            if (aTags.Count <= 2)
                return 1;
            var pageCount = aTags.ElementAt(aTags.Count-2).InnerText;

            int total = 0;

            int.TryParse(pageCount, out total);

            return total;
        }
    }
}
