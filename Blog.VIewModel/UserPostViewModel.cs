
using Blog.Entities.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Blog.View
{
   public class UserPostViewModel:IValidatableObject
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "This field can not be empty")]
        public string PostName { get; set; }
        [Required(ErrorMessage = "This field can not be empty")]
        public string PostText { get; set; }
        public string CreatedPost { get; set; }
        public int CommentsCount { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public byte[] Image { get; set; }
        [Required(ErrorMessage = "You must select an image")]
        public HttpPostedFileBase uploadPicture  { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            int pictureSize = 1000000;
            List<ValidationResult> validationResult = new List<ValidationResult>();

            if (uploadPicture.ContentLength > pictureSize )
            {
                validationResult.Add(new ValidationResult("Image is too big" ,new List<string> { "uploadPicture" }));
            }
            return validationResult;
        }
    }
}
