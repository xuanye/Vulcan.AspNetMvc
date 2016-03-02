using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using SampleWebApp.Models;
using StructureMap;
using StructureMap.Configuration.DSL;
using Vulcan.AspNetMvc;
using Vulcan.AspNetMvc.DependencyInjection;


namespace SampleWebApp
{
    public class Bootstraper
    {
        public static void Initialise()
        {
            Registry registry = new Registry();
            registry.IncludeRegistry<DefaultRegistry>();//注册了默认的Newtonsoft.Json序列化 Controller中的JsonResult
            registry.IncludeRegistry<ServiceRegistry>();
            
            IContainer container =  ConfigureDependencies.InitContainer(registry);

            //Register for MVC
            DependencyResolver.SetResolver(new StructureMapDependencyScope(container));

            //Register for Web API
            //GlobalConfiguration.Configuration.DependencyResolver = new StructureMapResolver(container);
           
        }
    }
}