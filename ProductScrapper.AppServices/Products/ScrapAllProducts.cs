using HtmlAgilityPack;
using MediatR;
using ScrapeEngine;
using ScrapeEngine.Root;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProductScrapper.AppServices.Products
{
    public class ScrapAllProducts
    {
        public class Request : IAsyncRequest<Response>
        {
            public string Url { get; set; }
        }

        public class Response: BaseResponse
        {

        }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            private Regex priceRegex { get; set; } = new Regex("NOW:.[^\\(]*");
            public Task<Response> Handle(Request message)
            {
                var targetUrl = "http://www.dickssportinggoods.com";
                var html = Browser.HttpWebRequestGet(message.Url);

                throw new NotImplementedException();
            }

            private void GetLinks(string html)
            {
                var doc = Helper.GetDocument(html);
                var nodes = Helper.GetCollection(doc, "//li[contains(@class,'prod-item')]");


                foreach (var node in nodes)
                {
                    
                }
            }
        }

        class ProductScrap : BaseClass
        {
            private Regex priceRegex { get; set; } = new Regex("NOW:.[^\\(]*");
            private string _url { get; set; }

            public ProductScrap(string url)
            {
                _url = url;
            }

            public async void GetDetails(HtmlNode node)
            {
                var img = Helper.GetSingleSubNode(node, ".//div[@class='prod-image']/a/img");
                var priceNode = Helper.GetSingleSubNode(node, ".//div[@class='prodPriceWrap']");
                var link = Helper.GetSingleSubNode(node, ".//h2[@class='prod-title']/a");


                var price = GetPrice(priceNode);

            }

            private string GetPrice(HtmlNode node)
            {
                if (node == null)
                    return "";

                var price = node.InnerText;
                if (price.Contains("NOW"))
                {
                    price = priceRegex.Match(price).Value
                        .Replace("NOW:", string.Empty);

                }

                return price.Replace("&#036;", "$"); ;
            }

            public void GetLinks(string html)
            {
                var doc = Helper.GetDocument(html);
                var nodes = Helper.GetCollection(doc, "//li[contains(@class,'prod-item')]");


                foreach (var node in nodes)
                {
                    GetDetails(node);
                }
            }

            public void GetPages()
            {
                targetUrl = "http://www.dickssportinggoods.com";
                var html = Browser.HttpWebRequestGet(_url);
                GetLinks(html);
            }
        }
       
    }
}
