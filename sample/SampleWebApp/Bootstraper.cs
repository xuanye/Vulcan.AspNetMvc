using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using SampleWebApp.Models;
using StructureMap;
using StructureMap.Configuration.DSL;
using Vulcan.AspNetMvc.DependencyInjection;


namespace SampleWebApp
{
    public class Bootstraper
    {
        public static void Initialise()
        {
            List<Registry> rlist = new List<Registry>();
            rlist.Add(new ServiceRegistry());         
            
            IContainer container =  ConfigureDependencies.InitContainer(rlist);

            //Register for MVC
            DependencyResolver.SetResolver(new StructureMapDependencyScope(container));

            //Register for Web API
            //GlobalConfiguration.Configuration.DependencyResolver = new StructureMapResolver(container);
           
        }
    }
}