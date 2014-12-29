using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Vulcan.AspNetMvc.Test.MockObject
{
    public class TestController:Controller
    {
        private IHelloService service;
        public TestController(IHelloService service)
        {
            this.service = service;
        }

        public ContentResult SayHello()
        {
            string greeting =  this.service.Hello("admin");
            return Content(greeting);
        }
    }
}
