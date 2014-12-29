using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using StructureMap.Configuration.DSL;
using Vulcan.AspNetMvc.Common;
using Vulcan.AspNetMvc.DependencyInjection;
using Vulcan.AspNetMvc.Test.MockObject;

namespace Vulcan.AspNetMvc.Test.DependencyInjection
{
    [TestFixture]
    public class TestIoc
    {
        [TestFixtureSetUp]      
        public void Setup()
        {
            var list = new List<Registry>();
            list.Add(new ServiceRegistry());
            ConfigureDependencies.InitContainer(list);
        }

        [Test]
        public void TestServiceFactory()
        {
            var service = ServiceFactory.GetInstance<ISampleService>();
            Assert.IsNotNull(service);

            var service2 = ServiceFactory.GetInstance<ISampleService>();

            Assert.IsTrue(service.Equals(service2));
        }

        [TestFixtureTearDown]
        public void Clear()
        {
            ConfigureDependencies.DisposeContainer();
        }

    }
}
