using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using StructureMap;
using Vulcan.AspNetMvc.DependencyInjection;
using Vulcan.AspNetMvc.Interfaces;
using Vulcan.AspNetMvc.Test.MockObject;

namespace Vulcan.AspNetMvc.Test.Controllers
{
    [TestFixture]
    public class TestContollerIoc
    {
        [SetUp]
        public void Setup()
        {
            Registry registry = new Registry();
            registry.For<IJsonSerialize>().Use<ServiceStackSerialize>().Singleton();
            registry.IncludeRegistry<ServiceRegistry>();
         
            ConfigureDependencies.InitContainer(registry);

            var container = ConfigureDependencies.GetContainer();
            DependencyResolver.SetResolver(new StructureMapDependencyScope(container));
        }

        [Test]
        public void TestContollerIocSayHello()
        {
            TestController controller = DependencyResolver.Current.GetService<TestController>();

            Assert.IsNotNull(controller);

            dynamic result = controller.SayHello();

            Assert.IsNotNull(result.Content);

            Assert.AreEqual("Hello,admin!", result.Content);
        }
        [Test]
        public void TestContollerIocSayJsonHello()
        {
            TestController controller = DependencyResolver.Current.GetService<TestController>();

            Assert.IsNotNull(controller);

            dynamic result = controller.SayHelloJson();

            Assert.IsNotNull(result.Data);

            Assert.AreEqual("Hello,admin!", result.Data.Message);
        }

        [TearDown]
        public void Clear()
        {
            ConfigureDependencies.DisposeContainer();
        }
    }
}
