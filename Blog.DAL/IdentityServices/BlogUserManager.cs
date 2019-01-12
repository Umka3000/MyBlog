
using Blog.Entities.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.IdentityServices
{
   public class BlogUserManager:UserManager<BlogUser>
    {
        public BlogUserManager(IUserStore<BlogUser> store) : base(store)
        {
        }

        public static BlogUserManager Create(IdentityFactoryOptions<BlogUserManager> options,
                                            IOwinContext context)
        {
            BlogContext dataBase = context.Get<BlogContext>();
            BlogUserManager manager = new BlogUserManager(new UserStore<BlogUser>(dataBase));
            return manager;
        }
    }
}
