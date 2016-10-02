using MediatR;
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
            public string Url { get; set; }
        }

        public class Response: BaseResponse
        {

        }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Task<Response> Handle(Request message)
            {
                var url = "http://www.dickssportinggoods.com/family/index.jsp?bc=CatGroup_MensShoes_R1_C1_AthleticSneakers&ppp=144&categoryId=63266056&page=1";
                var productDetailUrl =
                "http://www.dickssportinggoods.com/product/index.jsp?productId=68713676&ppp=144&cp=4406646.4413987.4417989&categoryId=63266056";
                
                var pManager = new ProductDetailsManager(productDetailUrl);
                // pManager.GetProducts(url);
                pManager.GetProductDetails();
                throw new NotImplementedException();
            }
        }
    }
}
