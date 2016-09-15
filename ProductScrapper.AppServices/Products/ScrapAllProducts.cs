using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            public Task<Response> Handle(Request message)
            {
                throw new NotImplementedException();
            }
        }
    }
}
