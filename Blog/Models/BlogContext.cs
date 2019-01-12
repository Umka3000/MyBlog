using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class BlogContext : IdentityDbContext<BlogUser>
    {
        public BlogContext():base("IdentityDb")
        {
            
        }

        public DbSet<PersonBlog> UserBlogs { get; set; }

        public static BlogContext Create()
        {
            return new BlogContext();
        }
       
    }
}