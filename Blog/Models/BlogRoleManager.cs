using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class BlogRoleManager : RoleManager<BlogRole>
    {
        public BlogRoleManager(IRoleStore<BlogRole, string> store) : base(store)
        {
        }

        public static BlogRoleManager Create(IdentityFactoryOptions<BlogRoleManager> options,
                                                IOwinContext context)
        {
            return new BlogRoleManager(new
                    RoleStore<BlogRole>(context.Get<BlogContext>()));
        }
    }
}