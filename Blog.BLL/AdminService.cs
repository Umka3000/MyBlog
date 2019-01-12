using Blog.BLL.ServiceInterfaces;
using Blog.DAL.IdentityServices;
using Blog.Entities.Model;
using Blog.View;
using Blog.VIewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Blog.BLL
{
    public class AdminService : IAdminService
    {
        private BlogRoleManager RoleManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<BlogRoleManager>();
            }
        }

        private BlogUserManager UserManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<BlogUserManager>();
            }
        }

        public List<UserViewModel> GetAllUsers()
        {
            List<UserViewModel> blogUsers = Mapper.GetViewUser(UserManager);
            return blogUsers;
        }

        public void DeleteUser(string userName)
        {
            BlogUser user = UserManager.FindByEmail(userName);
            if (user != null)
            {
                UserManager.Delete(user);
            }
        }

        public void BlockUser(string userName)
        {
            BlogUser user = UserManager.FindByEmail(userName);
            var newRole = new IdentityUserRole();
            user.Roles.Clear();
            newRole.RoleId = RoleManager.FindByName("blocked").Id;
            newRole.UserId = user.Id;
            user.Roles.Add(newRole);
            UserManager.Update(user);
        }

        public void UnblockUser(string userName)
        {
            BlogUser user = UserManager.FindByEmail(userName);
            var newRole = new IdentityUserRole();
            user.Roles.Clear();
            newRole.RoleId = RoleManager.FindByName("user").Id;
            newRole.UserId = user.Id;
            user.Roles.Add(newRole);
            UserManager.Update(user);
        }

        public void EditUser(EditUserViewModel userModel)
        {
            var newRole = new IdentityUserRole();

            BlogUser user = UserManager.FindByEmail(userModel.Email);
            user.Roles.Clear();
            newRole.RoleId = RoleManager.FindByName(userModel.Role).Id;
            newRole.UserId = user.Id;

            if (user != null || !string.IsNullOrEmpty(userModel.Email))
            {
                user.Email = userModel.Email;
                user.Roles.Add(newRole);
                UserManager.Update(user);
            }
        }

        public EditUserViewModel SearchUserByEmail(string userEmail)
        {
            var viewUser = new EditUserViewModel();
            BlogUser user = UserManager.FindByEmail(userEmail);
            if (user != null)
            {
                viewUser = Mapper.BlogUserInViewUser(user);
            }
            return viewUser;
        }
    }
}




