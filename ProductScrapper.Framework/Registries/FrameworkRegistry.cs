using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using System.Threading.Tasks;
using System.Net.Mail;
using ProductScrapper.AppServices;
using static ProductScrapper.AppServices.Products.ScrapAllProducts;
using ProductScrapper.AppServices.Products;
using MediatR;
using FluentValidation;
using MongoDB.Driver;

namespace ProductScrapper.Framework.Registries
{
    public class FrameworkRegistry: Registry
    {
        public FrameworkRegistry(IBootstrapperSettings settings)
        {

            //Scan(scanner =>
            //{
            //    //scanner.AssemblyContainingType(typeof(FluentValidation.AbstractValidator<>));
            //    scanner.AssemblyContainingType(typeof(ScrapAllProducts.Request));
            //    //scanner.AssemblyContainingType(typeof(IRequestHandler<,>));
            //    scanner.AssemblyContainingType(typeof(IAsyncRequestHandler<,>));

            //    //scanner.AddAllTypesOf(typeof(FluentValidation.IValidator<>));
            //    //scanner.AddAllTypesOf(typeof(IRequestHandler<,>));
            //    scanner.AddAllTypesOf(typeof(IAsyncRequestHandler<,>));

            //    scanner.WithDefaultConventions();
            //});

            //For(typeof(IAsyncRequestHandler<,>)).Use(typeof(Handler));

            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.AssemblyContainingType(typeof(ScrapAllProducts.Request));
                scanner.AssemblyContainingType(typeof(IRequestHandler<,>));
                scanner.AssemblyContainingType(typeof(IAsyncRequestHandler<,>));
                scanner.AssemblyContainingType(typeof(IValidator<>));
                scanner.AssemblyContainingType(typeof(IMediator));

                scanner.AddAllTypesOf(typeof(IValidator<>));
                scanner.AddAllTypesOf(typeof(IAsyncRequestHandler<,>));
                scanner.AddAllTypesOf(typeof(IAsyncNotificationHandler<>));
                scanner.AddAllTypesOf(typeof(IMediator));
            });

            For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
            For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
            For<IMediator>().Use<Mediator>();
            For<MongoFileClient>().Use(ctx => CreateSession(ctx));

            var featureHandlerTypes = For(typeof(IAsyncRequestHandler<,>));

        }

        private MongoFileClient CreateSession(IContext ctx)
        {
            return ctx.GetInstance<MongoFileClient>("amazon");
        }
    }
}