using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.Model
{
    public class Like
    {
        public int Id { get; set; }
        public bool UserLike { get; set; }
        public int PostId { get; set; }
        public string UserNameWho { get; set; }
        public Post Post { get; set; }
    }
}
