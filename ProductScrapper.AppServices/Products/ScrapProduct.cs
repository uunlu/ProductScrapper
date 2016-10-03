using MediatR;
using ProductScrapper.Core.Models;
using ProductScrapper.ScrapperEngine.Dickssportinggoods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductScrapper.AppServices.Products
{
    public class ScrapProduct
    {
        public class Request : IAsyncRequest<Response>
        {

        }

        public class Response: BaseResponse
        {
            public int Total { get; set; }
        }

        public class Handler : BaseHandler<Request, Response>
        {
            public override async Task<Response> Handle(Request request)
            {
                var site = "http://www.dickssportinggoods.com";
                var urls = await Session.GetCollection<Message>(new Message());

                foreach (var item in urls)
                {
                    var productDetailUrl = site + item.UrlToScrap;
                    var pManager = new ProductDetailsManager(productDetailUrl);
                    var details = pManager.GetProductDetails();

                    await Session.InsertOne(details);
                }
                return new Response {  Total = urls.Count};
            }

        }
    }
}
