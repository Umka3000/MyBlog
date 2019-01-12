using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserComment { get; set; }
        public string User  { get; set; }
        public string UserPostCreator { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string CreatedComment { get; set; }
    }
}
