using ScrapeEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductScrapper.ScrapperEngine.Dickssportinggoods
{
    public class ProductListManager
    {
        public void GetProducts(string url)
        {
            var html = Browser.HttpWebRequestGet(url);
            var docDetail = Helper.GetDocument(html);
            var total = Helper.GetSingleNode(docDetail, "//div[@class='pagination']").InnerText;
        }
    }
}
