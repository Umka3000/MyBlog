using Blog.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class FreeBlogController : Controller
    {

        private BlogUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<BlogUserManager>();
            }
        }


        public ActionResult Index()
        {
        
            return View();
        }


    }
}