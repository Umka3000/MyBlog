using Blog.BLL;
using Blog.BLL.ServiceInterfaces;
using Blog.DAL.IdentityServices;
using Blog.Entities.Model;
using Blog.View;
using Blog.VIewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.UI.Controllers
{
   
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public ActionResult Index()
        {
            List<UserViewModel> blogUsers = _adminService.GetAllUsers();
            return View(blogUsers);
        }

        public ActionResult DeleteUser(string userEmail)
        {
            if (!string.IsNullOrEmpty(userEmail))
            {
                _adminService.DeleteUser(userEmail);
            }
            _adminService.DeleteUser(userEmail);

            return RedirectToAction("Index");
        }
        
        public ActionResult ShowEditedUser(string email)
        {
            EditUserViewModel user = _adminService.SearchUserByEmail(email);
            return View(user);
        }

        [HttpPost]
        public ActionResult EditBlogUser(EditUserViewModel userView)
        {
            _adminService.EditUser(userView);

            return RedirectToAction("RegistredIndex");
        }

        public ActionResult BlockBlogUser(string email)
        {
            _adminService.BlockUser(email);

            return RedirectToAction("RegistredIndex");
        }

        public ActionResult UnblockBlogUser(string email)
        {
            _adminService.UnblockUser(email);

            return RedirectToAction("RegistredIndex");
        }
    }
}