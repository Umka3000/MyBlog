using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.Model
{
    public class Picture
    {
        [Key]
        [ForeignKey("Post")]
        public int Id { get; set; } 
        public byte[] Image { get; set; }
        public Post Post { get; set; }
    }
}
