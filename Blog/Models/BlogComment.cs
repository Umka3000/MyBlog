using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class BlogComment
    {
        public int Id { get; set; }
        public BlogUser PersonComment { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedComment { get; set; }
        public DateTime ChangedComment { get; set; }
    }
}