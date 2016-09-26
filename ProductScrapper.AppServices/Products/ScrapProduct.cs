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
                var pManager = new ProductListManager();
                var url = "http://www.dickssportinggoods.com/family/index.jsp?bc=CatGroup_MensShoes_R1_C1_AthleticSneakers&ppp=144&categoryId=63266056&page=1";
                pManager.GetProducts(url);
                throw new NotImplementedException();
            }
        }
    }
}
