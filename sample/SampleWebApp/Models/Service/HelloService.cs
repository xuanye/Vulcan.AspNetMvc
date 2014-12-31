using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SampleWebApp.Models.ServiceInterfaces;

namespace SampleWebApp.Models.Service
{
    public class HelloService:IHelloService
    {
        public string SayHello(string name)
        {
            return string.Format("Hello,{0}", name);
        } 
    }
}