using Blog.BLL;
using Blog.BLL.ServiceInterfaces;
using Blog.Entities.Model;
using Blog.View;
using Blog.VIewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Blog.UI.Controllers
{
    [Authorize(Roles = "admin, user, corrector")]
    public class BlogController : Controller
    {
        private IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public ActionResult CreatePost()
        {
            return View();
        }

        [HttpGet]
        public ActionResult _AddLike(int postId)
        {
            bool user = _blogService.CheckUserLike(User.Identity.Name, postId);
            if (user)
            {
                _blogService.AddUserLike(User.Identity.Name, postId);
            }
            int likes = _blogService.PostLikes(postId);
            return PartialView(likes);
        }

        [HttpGet]
        public ActionResult _AddDislike(int postId)
        {
            bool user = _blogService.CheckUserLike(User.Identity.Name, postId);
            if (user)
            {
                _blogService.AddUserDislike(User.Identity.Name, postId);
            }
            int dislikes = _blogService.PostDislikes(postId);
            return PartialView(dislikes);
        }

        [HttpPost]
        public ActionResult CreatePost(UserPostViewModel post)
        {

            if (ModelState.IsValid)
            {
                _blogService.CreateUserPost(post);
                return RedirectToAction("RegistredIndex", "Blog");
            }
            return View(post);
        }

        public ActionResult RegistredIndex()
        {

            List<UserPostViewModel> posts = new List<UserPostViewModel>();
            posts = _blogService.GetUserPosts(User.Identity.Name);

            if (posts.Count() == 0)
            {
                ViewBag.PostCount = "No Posts";
            }
            return View(posts);
        }
        [AllowAnonymous]
        public ActionResult _ShowComments(int id)
        {
            List<UserCommentViewModel> postComments = _blogService.PostComments(id);
            return PartialView(postComments);
        }

        [HttpGet]
        public ActionResult _AddComment(string comment, int id, string userName)
        {
            var viewUserComment = new UserCommentViewModel();

            if (ModelState.IsValid)
            {
                viewUserComment = _blogService.AddUserComment(comment, id, User.Identity.Name, userName);
                return PartialView(viewUserComment);
            }

            return RedirectToAction("OpenPost");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            List<UserPostViewModel> userPosts = _blogService.GetAllPosts();
            return View(userPosts);
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult OpenPost(int id)
        {
            UserPostViewModel userPost = _blogService.GetPost(id);
            ViewBag.Userlike = _blogService.CheckUserLike(User.Identity.Name, id);
            ViewBag.PostId = userPost.Id;
            ViewBag.DislikeNames = _blogService.GetDislikesNames(id);
            ViewBag.LikeNames = _blogService.GetLikesNames(id);
            ViewBag.UserName = userPost.UserName;
            userPost.LikesCount = _blogService.PostLikes(id);
            userPost.DislikesCount = _blogService.PostDislikes(id);
            userPost.CommentsCount = _blogService.GetCommentsCount(id);
            return View(userPost);
        }

        [HttpGet]
        public ActionResult DeletePost(int id)
        {
            _blogService.DeletePostById(id, User.Identity.Name, User.IsInRole("Admin"));
            return RedirectToAction("RegistredIndex");
        }
        [HttpGet]
        public ActionResult EditPost(int postId)
        {
            UserPostViewModel userPost = _blogService.GetPost(postId);

            return View(userPost);
        }

        [HttpPost]
        public ActionResult EditPost(EditViewModel editModel)
        {
            UserPostViewModel userPost = _blogService.SaveUserPostChanges(editModel.EditedInfo, editModel.Id);

            return RedirectToAction("OpenPost", "Blog", userPost);
        }
    }


}