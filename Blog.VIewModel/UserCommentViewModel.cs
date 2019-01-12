using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.View
{
   public class UserCommentViewModel
    {   
        public int PostId { get; set; }
        [Required(ErrorMessage ="This field can`t be empty")]
        public string Comment { get; set; }
        public string CreatedTime { get; set; }
        public string User { get; set; }
        public string UserPostCreator { get; set; }
    }
}
