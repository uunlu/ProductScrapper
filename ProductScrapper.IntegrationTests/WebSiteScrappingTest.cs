using Newtonsoft.Json.Linq;
using ScrapeEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace ProductScrapper.IntegrationTests
{
    public class WebSiteScrappingTest
    {
        private string BaseUrl { get; set; }
        public string ProductDetailsUrl { get; set; }
        public string ProductSearchUrl { get; set; }
        public WebSiteScrappingTest()
        {
            BaseUrl = "http://www.dickssportinggoods.com";
            ProductDetailsUrl =
                "http://www.dickssportinggoods.com/product/index.jsp?productId=93353866&ppp=144&cp=4406646.4413987.4417989&categoryId=63266056";

            ProductSearchUrl =
                "http://www.dickssportinggoods.com/family/index.jsp?bc=CatGroup_MensShoes_R1_C1_AthleticSneakers&categoryId=63266056&ppp=144";
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
        public void can_extract_total_number_of_items()
        {
            var html = Browser.HttpWebRequestGet(ProductSearchUrl);
            var docDetail = Helper.GetDocument(html);
            //var total = Helper.GetSingleNode(docDetail, "//div[@class='pagination']").InnerText;
            var productCount = Helper.GetSingleNodeByClass(docDetail, "span", "product-count").InnerText
                .Replace("Products", string.Empty).Trim();

            Assert.Equal(productCount, "1872");

        }

        [Fact]
        public void can_extract_total_number_of_pages()
        {
            var html = Browser.HttpWebRequestGet(ProductSearchUrl);
            var docDetail = Helper.GetDocument(html);
            //var total = Helper.GetSingleNode(docDetail, "//div[@class='pagination']").InnerText;
            var aTags = Helper.GetCollectionByClass(docDetail, "span", "pages", "a");
            var pageCount = aTags.ElementAt(3).InnerText;

            Assert.Equal(pageCount, "13");
        }

        #region Product Details Tests
        // product detail test
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
        public void can_extract_product_sizes_from_product_details()
        {
            var html = Browser.HttpWebRequestGet(ProductDetailsUrl);
            var docDetail = Helper.GetDocument(html);
            var sizes = Helper.GetCollection(docDetail, "//li[contains(@class, 'size prod-sprite')]");

            Assert.Equal(sizes.Count(), 12);
        }

        [Fact]
        public void does_product_details_page_contains_json_object()
        {
            var html = Browser.HttpWebRequestGet(ProductDetailsUrl);


            Assert.True(html.Contains("var productJson"));
        }

        [Fact]
        public void can_get_available_sizes_from_json()
        {
            List<string> availableSizes = new List<string>();
            var html = Browser.HttpWebRequestGet(ProductDetailsUrl);
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
                    availableSizes.Add(item.size.ToString());
            }
            Assert.Equal(availableSizes.Count, 4);
        }

        [Fact]
        public void can_extract_product_information()
        {
            var html = Browser.HttpWebRequestGet(ProductDetailsUrl);
            var docDetail = Helper.GetDocument(html);
            var productInformation = Helper.GetSingleNodeByClass(docDetail, "div", "prod-short-desc");

            Assert.True(productInformation.InnerText.Contains("Much like its predecessors"));
        }

        [Fact]
        public void can_extract_product_features()
        {
            var html = Browser.HttpWebRequestGet(ProductDetailsUrl);
            var docDetail = Helper.GetDocument(html);
            var features = Helper.GetCollectionByClass(docDetail, "div", "prod-long-desc", "ul/li");

            Assert.Equal(features.Count,11);
        }

        [Fact]
        public void can_extract_product_main_image()
        {
            var html = Browser.HttpWebRequestGet(ProductDetailsUrl);
            Regex jsonRegex = new Regex("productJson.*?(?=</script>)", RegexOptions.Singleline);
            var json = jsonRegex.Match(html).Value
            .Replace("productJson", string.Empty)
            .Replace("=", string.Empty)
            .Replace("&#039;", "\"")
            .Replace(";", string.Empty);
            dynamic serializer = JObject.Parse(json);
            // alternateViews

            var mainImageLarge = serializer.alternateViews.main.largerEnhancedImageURL.ToString().Trim();

            Assert.Equal(mainImageLarge, "/graphics/product_images/pDSP1-23985407v750.jpg");
        }

        [Fact]
        public void can_extract_product_all_images()
        {
            List<string> images = new List<string>();
            var html = Browser.HttpWebRequestGet(ProductDetailsUrl);
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
                var imageUrl = serializer.alternateViews[alt]?.largerEnhancedImageURL.ToString();
                if (imageUrl == null) break;
                images.Add(imageUrl);
                index++;
            }

            Assert.Equal(images.Count, 5);
        }
        #endregion 

    }
}
