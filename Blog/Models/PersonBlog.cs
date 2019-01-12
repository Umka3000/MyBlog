using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class PersonBlog
    {
        public int Id { get; set; }
        public string User { get; set; }
        public List<BlogLike> BlogLikes { get; set; }
        public List<BlogView> PersonsView { get; set; }
        public List<BlogComment> Comments { get; set; }
        public string BlogText { get; set; }
        public string BlogCreated { get; set; }
        public string BlogChanged { get; set; }
    }
}