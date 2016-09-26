using ProductScrapper.AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using StructureMap;
using ProductScrapper.Framework.Registries;

namespace ProductScrapper.Framework
{
    public class Bootstrapper
    {
        public static IContainer GlobalContainer { get; protected set; }

        private IBootstrapperSettings _settings;
        private IContainer _container;

        public bool IsBoostrapped { get; private set; }

        public IContainer Container { get { return _container; } }

        public void Start(IBootstrapperSettings settings)
        {
            if (IsBoostrapped)
                return;
            IsBoostrapped = true;

            _settings = settings;

            ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => new StructureMapServiceLocator(_container)));
           
            _container = new Container(cfg =>
            {
                cfg.AddRegistry(new FrameworkRegistry(_settings));
            });

            _container.AssertConfigurationIsValid();

            GlobalContainer = _container;

            string whatDoIHave = _container.WhatDoIHave();    //For debuging purposes
            string x = _container.WhatDidIScan();    //For debuging purposes
        }
    }
}
