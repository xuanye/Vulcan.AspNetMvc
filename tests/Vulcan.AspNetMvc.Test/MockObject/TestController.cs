using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Vulcan.AspNetMvc.Extensions;

namespace Vulcan.AspNetMvc.Test.MockObject
{
    public class TestController:VulcanController
    {
        private readonly IHelloService service;
        public TestController(IHelloService service)
        {
            this.service = service;
        }

        public ContentResult SayHello()
        {
            string greeting =  this.service.Hello("admin");
            return Content(greeting);
        }
        public JsonResult SayHelloJson()
        {
            string greeting = this.service.Hello("admin");
            return Json(new { Name = "admin", Message = greeting,Time = DateTime.Now });
        }
    }
}
