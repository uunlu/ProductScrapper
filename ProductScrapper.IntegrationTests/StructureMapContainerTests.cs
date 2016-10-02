using MediatR;
using ProductScrapper.AppServices.Products;
using ProductScrapper.Framework;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductScrapper.IntegrationTests
{
    public class StructureMapContainerTests
    {
        private readonly Bootstrapper Bootstrapper;

        public StructureMapContainerTests()
        {
            Bootstrapper = new Bootstrapper();
            Bootstrapper.Start(new BootstrapperSettings());
        }

        [Fact]
        public void validate_request_handler_dependencies1()
        {
            Assert.Equal(6, 6);
        }


        [Fact]
        public void validate_request_handler_dependencies()
        {
            var a = Bootstrapper.Container.GetInstance<IAsyncRequestHandler<ScrapAllProducts.Request, ScrapAllProducts.Response>>();
        }

        [Fact]
        public void handler_factory_can_create_root_handler_when_given_type()
        {
            var rootHandlerType = typeof(IAsyncRequestHandler<ScrapAllProducts.Request, ScrapAllProducts.Response>);

            var handler = Bootstrapper.Container.GetInstance(rootHandlerType);

            Assert.NotNull(handler);
            Assert.IsAssignableFrom<IAsyncRequestHandler<ScrapAllProducts.Request, ScrapAllProducts.Response>>(handler);
        }
    }
}
