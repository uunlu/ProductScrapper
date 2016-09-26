using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediatR;
using StructureMap.Attributes;
using ProductScrapper.AppServices;
using System.Threading.Tasks;

namespace ProductScrapper.Controllers
{
    public class BaseController : Controller
    {
        [SetterProperty]
        public IRequestDispatcher Dispatcher { get; set; }

        [SetterProperty]
        public IMediator mediator { get; set; }

        protected async Task<TResponse> DispatchAsync<TResponse>(IAsyncRequest<TResponse> request)
        where TResponse : IResponse
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            //return await Dispatcher.DispatchAsync(request);
            return await mediator.SendAsync(request);
        }
    }
}