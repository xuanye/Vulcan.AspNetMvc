using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;

namespace Vulcan.AspNetMvc.Test.MockObject
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry()
        {
            For<ISampleService>().Use(new SampleService());
            For<IHelloService>().Use(new HelloService());
        }
    }
}
