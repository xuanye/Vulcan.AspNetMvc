using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SampleWebApp.Models.Service;
using SampleWebApp.Models.ServiceInterfaces;
using StructureMap;
using Vulcan.AspNetMvc.Interfaces;

namespace SampleWebApp.Models
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry()
        {
            For<IHelloService>().Use(new HelloService());

            For<IAppContextService>().Use<AppContextService>();
            For<IResourceService>().Use<ResourceService>();

            For<IDictService>().Use<DictService>();
       
        }
    }
}