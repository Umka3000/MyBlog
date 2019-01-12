using Blog.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Repositories
{
    public class LikesRepository
    {
        public void AddLike(Like like, Post post)
        {
            using (var context = new BlogContext())
            {
                context.UserPosts.Attach(post);
                context.Likes.Add(like);
                context.SaveChanges();
            }
        }

        public int GetLikesOrDislikes(int postId, bool likeCount)
        {
            int countOfLikes = 0;
            using (var context = new BlogContext())
            {
               countOfLikes = context.Likes.Where(like => like.PostId == postId && like.UserLike == likeCount).Count();
            }
            return countOfLikes;
        }

        public bool CheckLikes(string userName, int postId)
        {
            bool result = false;
            using (var context = new BlogContext())
            {
                int countOfLikes = context.Likes.Where(like => like.UserNameWho == userName && like.PostId == postId).Count();
                if (countOfLikes == 0)
                {
                    result = true;
                }
            }
            return result;
        }

        public List<Like> GetListLikesByPostId( int postId)
        {
            List<Like> likes = new List<Like>();
            using (var context = new BlogContext())
            {
                likes = context.Likes.Where(like => like.PostId == postId && like.UserLike == true).ToList();
            }
            return likes;
        }

        public List<Like> GetListDislikesByPostId(int postId)
        {
            List<Like> dislikes = new List<Like>();
            using (var context = new BlogContext())
            {
                dislikes = context.Likes.Where(like => like.PostId == postId && like.UserLike == false).ToList();
            }
            return dislikes;
        }
    }
}
