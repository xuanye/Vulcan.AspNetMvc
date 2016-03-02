using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SampleWebApp.Models.ServiceInterfaces;
using Vulcan.AspNetMvc.Context;
using Vulcan.AspNetMvc.Extensions;

namespace SampleWebApp.Controllers
{
    public class HomeController : VulcanController
    {
        private IHelloService service;
        public HomeController(IHelloService service)
        {
            this.service = service;
        }
        //
        // GET: /Home/

        [VulcanAuthorize]
        public ActionResult Index()
        {
            ViewBag.Message = this.service.SayHello(base.CurrentUser.FullName);
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }
        public JsonResult TestJson()
        {
            return Json(new { Name = "Xuanye", Age = 13 });
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            string userId = form["UserId"];
            string password = form["Password"];

            if (userId == "admin" && password == "123456")
            {
                FormsAuthentication.SetAuthCookie(userId, false);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "用户名和密码错误";
                return View("Login");
            }
            
        }
    }
}
