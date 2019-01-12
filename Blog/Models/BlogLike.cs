using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class BlogLike
    {
        public int Id { get; set; }
        public BlogUser PersonsLike { get; set; }
    }
}