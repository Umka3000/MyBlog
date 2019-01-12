using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Blog.Models;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(Blog.App_Start.Startup))]

namespace Blog.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(BlogContext.Create); //Упростил...могут быть проблемы
            app.CreatePerOwinContext<BlogUserManager>(BlogUserManager.Create);
            app.CreatePerOwinContext<BlogRoleManager>(BlogRoleManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });

        }
    }
}
