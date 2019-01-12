using Blog.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Repositories
{
    public class CommentsRepository
    {
        public List<Comment> GetPostComments(int id)
        {
            List<Comment> comments = new List<Comment>();
            using (var context = new BlogContext())
            {
                comments = context.Comments.Where(comment => comment.PostId == id).ToList();
            }
            comments.Reverse();
            return comments;
        }

        public void AddComment(Comment userComment, Post post)
        {
            using (var context = new BlogContext())
            {
                context.UserPosts.Attach(post);
                context.Comments.Add(userComment);
                context.SaveChanges();
            }
        }

        public int CountOfComments(int id)
        {
            int count = 0;
            using (var context = new BlogContext())
            {
                count = context.Comments.Where(comment => comment.PostId == id).Count();
            }
            return count;
        }
    }
}
