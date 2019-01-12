using Blog.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Repositories
{
    public class PostsRepository
    {
        public List<Post> AllPosts()
        {
            List<Post> posts = new List<Post>();
            using (var context = new BlogContext())
            {
                posts = context.UserPosts.ToList();
            }
            posts.Reverse();
            return posts;
        }

        public List<Post> PostsByUserName(string name)
        {
            List<Post> posts = new List<Post>();
            using (var context = new BlogContext())
            {
                posts = context.UserPosts.Where(post => post.UserName == name).ToList();
            }
            posts.Reverse();
            return posts;
        }

        public void SavePost(Post post)
        {
            using (var context = new BlogContext())
            {
                context.UserPosts.Add(post);
                context.SaveChanges();
            }
        }

        public void DeletePostById(int id, string user, bool role)
        {
            var post = new Post();
            using (var context = new BlogContext())
            {
                post = context.UserPosts.Where(userPost => userPost.Id == id).FirstOrDefault();
                if (post.UserName == user || role)
                {
                    context.UserPosts.Remove(post);
                    context.SaveChanges();
                }
            }
        }

        public Post PostById(int id)
        {
            var post = new Post();
            using (var context = new BlogContext())
            {
                post = context.UserPosts.Where(userPost => userPost.Id == id).FirstOrDefault();
            }
            return post;
        }

        public Post SavePostChanges(string info, int id)
        {
            var post = new Post();
            using (var context = new BlogContext())
            {
                post = context.UserPosts.Where(userPost => userPost.Id == id).FirstOrDefault();
                post.PostText = info;
                context.SaveChanges();
            }
            return post;
        }
    }
}
