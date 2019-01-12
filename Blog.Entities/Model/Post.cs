using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.Model
{
    public class Post
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PostName { get; set; }
        public string PostText { get; set; }
        public string CreatedPost { get; set; }
        public List<Like> Likes { get; set; }
        public List<Comment> Comments { get; set; }
        public Picture Picture { get; set; }
        public byte[] Image { get; set; }
    }
}
