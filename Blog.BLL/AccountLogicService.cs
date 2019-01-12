using Blog.BLL.ServiceInterfaces;
using Blog.DAL.IdentityServices;
using Blog.Entities.Model;
using Blog.View;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Blog.BLL
{
    public class AccountLogicService : IAccountService
    {
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

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

        public void UserOut()
        {
            AuthenticationManager.SignOut();
        }

        public void RegistrationUser(RegisterViewModel viewModel)
        {
            var newRole = new IdentityUserRole();

            var user = new BlogUser();
            user.Email = viewModel.Email;
            user.UserName = viewModel.Email;

            if (UserManager.Users.Count() > 0)
            {
                newRole = new IdentityUserRole();
                newRole.RoleId = RoleManager.FindByName("User").Id;
                newRole.UserId = user.Id;
                user.Roles.Add(newRole);
                UserManager.Create(user, viewModel.Password);
            }

            if (UserManager.Users.Count() == 0)
            {
                string[] roles = { "admin", "user", "corrector", "blocked" };

                for (int i = 0; i < roles.Length; i++)
                {
                    BlogRole nextRole = new BlogRole();
                    nextRole.Name = roles[i];
                    RoleManager.Create(nextRole);
                }
                newRole.RoleId = RoleManager.FindByName("admin").Id;
                newRole.UserId = user.Id;
                user.Roles.Add(newRole);
                UserManager.Create(user, viewModel.Password);
            }
        }

        public void UserLogin(LoginViewModel model)
        {
            BlogUser user = UserManager.Find(model.Email, model.Password);
            if (user != null)
            {
                ClaimsIdentity claim = UserManager.CreateIdentity(user,
                                                  DefaultAuthenticationTypes.ApplicationCookie);
                AuthenticationManager.SignOut();
                AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false,

                }, claim);
            }
        }
    }
}
