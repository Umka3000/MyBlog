
using Blog.Entities.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL
{
    public class BlogContext : IdentityDbContext<BlogUser>
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["IdentityDb"].ConnectionString;
            }
        }

        public BlogContext() : base("IdentityDb") { }

        public DbSet<Post> UserPosts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        public static BlogContext Create()
        {
            return new BlogContext();
        }
    }
}
