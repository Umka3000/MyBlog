using Blog.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private BlogContext _context = new BlogContext();
        
        private BlogUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<BlogUserManager>();
            }
        }

        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult CreateBlog()
        {
            return View();
        }

        public ActionResult CreateBlog(CreateBlog model)
        {
           
            string createdTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

            PersonBlog blog = new PersonBlog {
                BlogText = model.BlogText,
                User = User.Identity.Name,
                BlogCreated = createdTime
            };


            //_context.UserBlogs.Add(blog);
            _context.SaveChanges();

            return RedirectToAction("Index", "Blog");  //Добавление прошло успешно
        }

    }
}