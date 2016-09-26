using ScrapeEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductScrapper.IntegrationTests
{
    public class WebSiteScrappingTest
    {
        private string BaseUrl { get; set; }
        public string ProductDetailsUrl { get; set; }
        public WebSiteScrappingTest()
        {
            BaseUrl = "http://www.dickssportinggoods.com";
            ProductDetailsUrl =
                "http://www.dickssportinggoods.com/product/index.jsp?productId=93353866&ppp=144&cp=4406646.4413987.4417989&categoryId=63266056";
        }
        [Fact]
        public void can_extract_all_nodes_in_a_page()
        {
            var url =
                "http://www.dickssportinggoods.com/family/index.jsp?bc=CatGroup_MensShoes_R1_C1_AthleticSneakers&categoryId=63266056&ppp=144";
            var html = Browser.HttpWebRequestGet(url);
            var doc = Helper.GetDocument(html);
            var nodes = Helper.GetCollection(doc, "//li[contains(@class,'prod-item')]");

            Assert.Equal(nodes.Count, 144);
        }

        [Fact]
        public void can_extract_product_details_url()
        {
            var url =
              "http://www.dickssportinggoods.com/family/index.jsp?bc=CatGroup_MensShoes_R1_C1_AthleticSneakers&categoryId=63266056&ppp=144";
            var html = Browser.HttpWebRequestGet(url);
            var doc = Helper.GetDocument(html);
            var nodes = Helper.GetCollection(doc, "//li[contains(@class,'prod-item')]");
            var productUrl = nodes.FirstOrDefault().SelectSingleNode(".//h2[@class='prod-title']/a").Attributes["href"].Value;
            Assert.Equal(productUrl, "/product/index.jsp?productId=93353866&ppp=144&cp=4406646.4413987.4417989&categoryId=63266056");
        }

        [Fact]
        public void can_extract_title_from_product_details()
        {
            //var url =
            // "http://www.dickssportinggoods.com/family/index.jsp?bc=CatGroup_MensShoes_R1_C1_AthleticSneakers&categoryId=63266056&ppp=144";
            //var html = Browser.HttpWebRequestGet(url);
            //var doc = Helper.GetDocument(html);
            //var nodes = Helper.GetCollection(doc, "//li[contains(@class,'prod-item')]");
            //var productUrl = nodes.FirstOrDefault().SelectSingleNode(".//h2[@class='prod-title']/a").Attributes["href"].Value;

            //var detailHtml = Browser.HttpWebRequestGet(BaseUrl + productUrl);
            //var docDetail = Helper.GetDocument(detailHtml);

            var html = Browser.HttpWebRequestGet(ProductDetailsUrl);
            var docDetail = Helper.GetDocument(html);

            var titleNode = Helper.GetSingleNodeByClass(docDetail, "h1", "prod-title");

            Assert.Equal(titleNode.InnerText, "Nike Men's Free RN Flyknit Running Shoes");
        }

        [Fact]
        public void can_extract_price_from_product_details()
        {
            var html = Browser.HttpWebRequestGet(ProductDetailsUrl);
            var docDetail = Helper.GetDocument(html);
            var price = Helper.GetSingleNode(docDetail, "//span[@itemprop='price']").InnerText.Replace("&#036;", "$");
            Assert.Equal(price, "$129.99");
        }

        // size prod-sprite

        [Fact]
        public void can_extract_available_sizes_from_product_details()
        {
            var html = Browser.HttpWebRequestGet(ProductDetailsUrl);
            var docDetail = Helper.GetDocument(html);
            var sizes = Helper.GetCollection(docDetail, "//li[@class='size prod-sprite disabled']");

            Assert.Equal(sizes.Count(), 6);
        }

        [Fact]
        public void can_extract_total_number_of_items()
        {
            var html = Browser.HttpWebRequestGet(ProductDetailsUrl);
            var docDetail = Helper.GetDocument(html);
            var total = Helper.GetSingleNode(docDetail, "//div[@class='pagination']").InnerText;

            Assert.Equal(total, "1840");

        }

        [Fact]
        public void can_find_total_numbers_of_page()
        {
            var html = Browser.HttpWebRequestGet(ProductDetailsUrl);
            var docDetail = Helper.GetDocument(html);
            var total = Helper.GetSingleNode(docDetail, "//span[@class='product-count']").InnerText;
            var pages = Helper.GetCollection(docDetail, "//span[@class='pages']")
                .Where(x=>!x.InnerHtml.Contains("next")).LastOrDefault();

            Assert.Equal(pages.InnerText, "13");
            int totalNumberOfItems = 0;
            int.TryParse(total, out totalNumberOfItems);
            const int numberOfItemsInPage = 144;
            Assert.Equal(totalNumberOfItems / numberOfItemsInPage + 1, int.Parse(pages.InnerText));
        }
    }
}
