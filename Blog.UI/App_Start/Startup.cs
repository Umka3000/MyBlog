using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Blog.DAL;
using Blog.DAL.IdentityServices;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(Blog.UI.App_Start.Startup))]

namespace Blog.UI.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(BlogContext.Create);
            app.CreatePerOwinContext<BlogUserManager>(BlogUserManager.Create);
            app.CreatePerOwinContext<BlogRoleManager>(BlogRoleManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Blog/Index"),
            });

        }
    }
}
