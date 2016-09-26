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

namespace ProductScrapper.Framework.Registries
{
    public class FrameworkRegistry: Registry
    {
        public FrameworkRegistry(IBootstrapperSettings settings)
        {
            
            Scan(scanner =>
            {
                scanner.AssemblyContainingType(typeof(FluentValidation.AbstractValidator<>));
                scanner.AssemblyContainingType(typeof(ScrapAllProducts.Request));
                scanner.AssemblyContainingType<IMediator>();
                scanner.AssemblyContainingType(typeof(IRequestHandler<,>));
                scanner.AssemblyContainingType(typeof(IAsyncRequestHandler<,>));
                scanner.AssemblyContainingType(typeof(IRequestDispatcher));

                //scanner.AddAllTypesOf(typeof(FluentValidation.IValidator<>));
                scanner.AddAllTypesOf(typeof(IRequestDispatcher));
                scanner.AddAllTypesOf(typeof(IRequestHandler<,>));
                scanner.AddAllTypesOf(typeof(IAsyncRequestHandler<,>));

                scanner.WithDefaultConventions();
            });

            // Messaging
            For<SmtpClient>().Use(() => SmtpClientFactory.CreateSmtpClient(settings));
            //For<IRequestDispatcher>().Use(ctx => new RequestDispatcher(type => ctx.GetInstance(type)));


            //For(typeof(IRequestHandler<,>)).Use(typeof(Handler)); // Register default handler.

            For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
            For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
            For<IMediator>().Use<Mediator>();   

            For(typeof(IAsyncRequestHandler<,>)).Use(typeof(Handler)); // Register default handler.

            //var featureHandlerTypes = For(typeof(IAsyncRequestHandler<,>));
            //featureHandlerTypes.DecorateAllWith(typeof(TransactionHandler<,>)); 
            //featureHandlerTypes.DecorateAllWith(typeof(ValidatorHandler<,>));

        }
    }
}
