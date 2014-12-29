using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using StructureMap.Configuration.DSL;
using Vulcan.AspNetMvc.DependencyInjection;
using Vulcan.AspNetMvc.Test.MockObject;

namespace Vulcan.AspNetMvc.Test.Controllers
{
    public class TestContollerIoc
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            var list = new List<Registry>();
            list.Add(new ServiceRegistry());
            ConfigureDependencies.InitContainer(list);

            var container = ConfigureDependencies.GetContainer();
            DependencyResolver.SetResolver(new StructureMapDependencyScope(container));
        }

        [Test]
        public void TestContollerIocSayHello()
        {
            TestController controller = DependencyResolver.Current.GetService<TestController>();

            Assert.IsNotNull(controller);

            dynamic result = controller.SayHello();

            Assert.IsNotNullOrEmpty(result.Content);

            Assert.AreEqual("Hello,admin!", result.Content);
        }

        [TestFixtureTearDown]
        public void Clear()
        {
            ConfigureDependencies.DisposeContainer();
        }
    }
}
