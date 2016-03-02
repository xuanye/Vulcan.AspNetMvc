using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using StructureMap;
using Vulcan.AspNetMvc.Common;
using Vulcan.AspNetMvc.DependencyInjection;
using Vulcan.AspNetMvc.Test.MockObject;

namespace Vulcan.AspNetMvc.Test.DependencyInjection
{
    [TestFixture]
    public class TestIoc
    {
        [SetUp]      
        public void Setup()
        {
            Registry registry = new Registry();
            registry.IncludeRegistry<ServiceRegistry>();
          
            ConfigureDependencies.InitContainer(registry);
        }

        [Test]
        public void TestServiceFactory()
        {
            var service = ServiceFactory.GetInstance<ISampleService>();
            Assert.IsNotNull(service);

            var service2 = ServiceFactory.GetInstance<ISampleService>();

            Assert.IsTrue(service.Equals(service2));
        }

        [TearDown]
        public void Clear()
        {
            ConfigureDependencies.DisposeContainer();
        }

    }
}
