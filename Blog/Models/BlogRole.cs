using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class BlogRole:IdentityRole
    {
        public BlogRole()
        {

        }

        public string Description { get; set; }
       
    }
}