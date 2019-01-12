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
        public static Post ConvertViewPostToPost(UserPostViewModel inputPost)
        {
            var post = new Post();
            post.Id = inputPost.Id;
            post.UserName = inputPost.UserName;
            post.PostText = inputPost.PostText;
            post.CreatedPost = inputPost.CreatedPost;
            post.PostName = inputPost.PostName;
            return post;
        }

        public static List<UserCommentViewModel> ConvertCommentsToViewComments(List<Comment> comments)
        {
            var tempComments = comments.Select(x => new UserCommentViewModel
            {
                Comment = x.UserComment,
                CreatedTime = x.CreatedComment,
                PostId = x.PostId,
                User = x.UserPostCreator,
                UserPostCreator = x.User

            }).ToList();

            return tempComments;
        }

        public static List<UserViewModel> GetViewUser(BlogUserManager userManager)
        {
            var tempUsers = userManager.Users.ToList();

            int i = 0;
            var tempBlogUsers = tempUsers.Select(x => new UserViewModel
            {
                Id = i++,
                Email = x.Email,
                Roles = (userManager.GetRoles(x.Id).ToList().Count() == 0)? new List<string>() {"none"}: userManager.GetRoles(x.Id).ToList(),
            }).ToList(); 

            return tempBlogUsers;
        }

        public static UserPostViewModel ConvertPostToViewPost(Post inputPost)
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

        public static List<UserPostViewModel> ConvertPostsToViewPost(List<Post> posts)
        {
            var tempViewPosts = posts.Select(x => new UserPostViewModel
            {
                Id = x.Id,
                UserName = x.UserName,
                PostText = x.PostText,
                CreatedPost = x.CreatedPost,
                PostName = x.PostName

            }).ToList();

            return tempViewPosts;
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

        public static List<Post> ConvertViewPostToPost(List<UserPostViewModel> postsUI)
        {
            var tempPosts = postsUI.Select(x => new Post
            {
                Id = x.Id,
                PostName = x.PostName,
                PostText = x.PostText,
                CreatedPost = x.CreatedPost,
                UserName = x.UserName,
            }).ToList();

            return tempPosts;
        }
    }
}
