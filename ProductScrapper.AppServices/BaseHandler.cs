using MediatR;
using MongoDB.Driver;
using StructureMap.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductScrapper.AppServices
{
    public abstract class BaseHandler<TRequest, TResponse> : IAsyncRequestHandler<TRequest, TResponse>
            where TRequest : IAsyncRequest<TResponse>
            where TResponse : IResponse
    {
        [SetterProperty]
        public MongoFileClient Session { get; set; } 

        public abstract Task<TResponse> Handle(TRequest request);


    }
}
