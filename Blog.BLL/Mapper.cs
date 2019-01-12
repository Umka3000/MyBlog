using Blog.Entities.Model;
using Blog.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.DAL.IdentityServices;
using Microsoft.AspNet.Identity;
using Blog.VIewModel;

namespace Blog.BLL
{
   public class Mapper
    {
        public static Post ViewPostToPost(UserPostViewModel inputPost)
        {
            var post = new Post();
            post.Id = inputPost.Id;
            post.UserName = inputPost.UserName;
            post.PostText = inputPost.PostText;
            post.CreatedPost = inputPost.CreatedPost;
            post.PostName = inputPost.PostName;
            return post;
        }

        public static List<UserCommentViewModel> ListCommentsToViewListComments(List<Comment> comments)
        {
            List<UserCommentViewModel> postComments = new List<UserCommentViewModel>();
            foreach (var comment in comments)
            {
                var userComment = new UserCommentViewModel();
                userComment.Comment = comment.UserComment;
                userComment.CreatedTime = comment.CreatedComment;
                userComment.PostId = comment.PostId;
                userComment.User = comment.UserPostCreator;
                userComment.UserPostCreator = comment.User;
                postComments.Add(userComment);


            }
            return postComments;
        }

        public static List<UserViewModel> GetViewUser(BlogUserManager userManager)
        {
            List<UserViewModel> blogUsers = new List<UserViewModel>();
            var tempUsers = userManager.Users.ToList();

            for (int i = 0; i < tempUsers.Count(); i++)
            {
                var blogUser = new UserViewModel();
                blogUser.Id = i;
                blogUser.Email = tempUsers[i].Email;
                blogUser.Roles = userManager.GetRoles(tempUsers[i].Id).ToList();
                if (blogUser.Roles.Count() == 0)
                {
                    blogUser.Roles.Add("none");
                }
                blogUsers.Add(blogUser);
            }
            return blogUsers;
        }

        public static UserPostViewModel PostToViewPost(Post inputPost)
        {
            var post = new UserPostViewModel();
            post.Id = inputPost.Id;
            post.PostText = inputPost.PostText;
            post.CreatedPost = inputPost.CreatedPost;
            post.UserName = inputPost.UserName;
            post.PostName = inputPost.PostName;
            post.Image = inputPost.Image;
            return post;
        }

        public static List<UserPostViewModel> ListPostToViewListPost(List<Post> posts)
        {
            List<UserPostViewModel> viewPost = new List<UserPostViewModel>();

            for (int i = 0; i < posts.Count(); i++)
            {
                UserPostViewModel post = new UserPostViewModel();
                post.Id = posts[i].Id;
                post.UserName = posts[i].UserName;
                post.PostText = posts[i].PostText;
                post.CreatedPost = posts[i].CreatedPost;
                post.PostName = posts[i].PostName;

                viewPost.Add(post);
            }

            return viewPost;
        }

        public static EditUserViewModel BlogUserInViewUser(BlogUser user)
        {
            var blogUserView = new EditUserViewModel();
            if (user != null)
            {
                blogUserView.Email = user.Email;
            }
            return blogUserView;
        }

        public static List<Post> ViewListPostToListPost(List<UserPostViewModel> postsUI)
        {
            List<Post> posts = new List<Post>();

            for (int i = 0; i < posts.Count(); i++)
            {
                Post post = new Post();
                post.Id = postsUI[i].Id;
                post.PostName = postsUI[i].PostName;
                post.PostText = postsUI[i].PostText;
                post.CreatedPost = postsUI[i].CreatedPost;
                post.UserName = postsUI[i].UserName;
                posts.Add(post);
            }
            return posts;
        }
    }
}
