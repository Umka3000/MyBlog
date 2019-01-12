using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class BlogView
    {
        public int Id { get; set; }
        public BlogUser PersonView { get; set; }
    }
}