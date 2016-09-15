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
                scanner.AssemblyContainingType(typeof(IRequestHandler<,>));
                scanner.AssemblyContainingType(typeof(IAsyncRequestHandler<,>));

                scanner.AddAllTypesOf(typeof(FluentValidation.IValidator<>));
                scanner.AddAllTypesOf(typeof(IRequestHandler<,>));
                scanner.AddAllTypesOf(typeof(IAsyncRequestHandler<,>));

                scanner.WithDefaultConventions();
            });

            // Messaging
            For<SmtpClient>().Use(() => SmtpClientFactory.CreateSmtpClient(settings));

            //For(typeof(IRequestHandler<,>)).Use(typeof(Handler)); // Register default handler.

        }
    }
}
