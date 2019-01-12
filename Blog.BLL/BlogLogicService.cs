
using Blog.BLL.ServiceInterfaces;
using Blog.DAL;
using Blog.DAL.IdentityServices;
using Blog.DAL.Repositories;
using Blog.Entities.Model;
using Blog.View;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Blog.BLL
{
    public class BlogLogicService : IBlogService
    {
        private PostsRepository _postsRepository;
        private LikesRepository _likesRepository;
        private CommentsRepository _commentsRepository;

        public BlogLogicService()
        {
            _postsRepository = new PostsRepository();
            _likesRepository = new LikesRepository();
            _commentsRepositorl = new CommentsRepository();
        }

        public void DeletePostById(int id, string name, bool role)
        {
            _postsRepository.DeletePostById(id, name, role);
        }

        public List<UserCommentViewModel> PostComments(int id)
        {
            List<UserCommentViewModel> comments = Mapper.ListCommentsToViewListComments(_commentsRepository.GetPostComments(id));
            return comments;
        }

        public int GetCommentsCount(int id)
        {
            int count = 0;
            count = _commentsRepository.CountOfComments(id);
            return count;
        }

        public int AddUserLike(string userName, int postId)
        {
            int countOfLikes = 0;
            bool getLike = true;
            Post post = _postsRepository.PostById(postId);
            Like like = FillLikeOrDislike(userName, postId, getLike, post);
            _likesRepository.AddLike(like, post);
            countOfLikes = _likesRepository.GetLikesOrDislikes(postId, getLike);
            return countOfLikes;
        }

        public int PostLikes(int postId)
        {
            int countOfLikes = 0;
            countOfLikes = _likesRepository.GetLikesOrDislikes(postId, true);
            return countOfLikes;
        }

        public bool CheckUserLike(string userName, int postId)
        {
            bool result = _likesRepository.CheckLikes(userName, postId);
            return result;
        }

        public int PostDislikes(int postId)
        {
            int countOfDislikes = 0;
            countOfDislikes = _likesRepository.GetLikesOrDislikes(postId, false);
            return countOfDislikes;
        }

        public int AddUserDislike(string userName, int postId)
        {
            int countOfDislikes = 0;
            bool getDislike = false;
            Post post = _postsRepository.PostById(postId);
            Like dislike = FillLikeOrDislike(userName, postId, getDislike, post);
            _likesRepository.AddLike(dislike, post);
            countOfDislikes = _likesRepository.GetLikesOrDislikes(postId, getDislike);
            return countOfDislikes;
        }

        public UserCommentViewModel AddUserComment(string comment, int id, string userWho, string userTo)
        {
            Post post = _postsRepository.PostById(id);
            string time = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            UserCommentViewModel postComment = FillViewComment(comment, id, userWho, userTo, time);
            Comment userComment = FillPostComment(comment, id, userWho, userTo, time, post);
            _commentsRepository.AddComment(userComment, post);

            return postComment;
        }

        public List<UserPostViewModel> GetAllPosts()
        {
            List<UserPostViewModel> userPosts = Mapper.ListPostToViewListPost(_postsRepository.AllPosts());

            foreach (var post in userPosts)
            {
                post.CommentsCount = _commentsRepository.CountOfComments(post.Id);
                post.DislikesCount = PostDislikes(post.Id);
                post.LikesCount = PostLikes(post.Id);
            }
            return userPosts;
        }

        public UserPostViewModel GetPost(int id)
        {
            UserPostViewModel userPost = Mapper.PostToViewPost(_postsRepository.PostById(id));
            userPost.CommentsCount = _commentsRepository.CountOfComments(userPost.Id);
            return userPost;
        }

        public List<UserPostViewModel> GetUserPosts(string name)
        {
            List<Post> posts = _postsRepository.PostsByUserName(name);
            List<UserPostViewModel> viewPosts = Mapper.ListPostToViewListPost(posts);
            foreach (var post in viewPosts)
            {
                post.CommentsCount = _commentsRepository.CountOfComments(post.Id);
                post.DislikesCount = PostDislikes(post.Id);
                post.LikesCount = PostLikes(post.Id);
            }
            return viewPosts;
        }

        public UserPostViewModel GetUserPostForEdit(int id)
        {
            UserPostViewModel userPost = Mapper.PostToViewPost(_postsRepository.PostById(id));
            return userPost;
        }

        public UserPostViewModel SaveUserPostChanges(string info, int id)
        {
            UserPostViewModel viewPost = Mapper.PostToViewPost(_postsRepository.SavePostChanges(info, id));
            return viewPost;
        }

        public void CreateUserPost(UserPostViewModel post)
        {
            string createTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            post.CreatedPost = createTime;
            Post userPost = Mapper.ViewPostToPost(post);
            using (var binaryReader = new BinaryReader(post.uploadPicture.InputStream))
            {
                userPost.Image = binaryReader.ReadBytes(post.uploadPicture.ContentLength);
            }
            _postsRepository.SavePost(userPost);

        }

        public string GetLikesNames(int postId)
        {
            int countOfNames = 0;
            string names = "";
            List<Like> likes = _likesRepository.GetListLikesByPostId(postId);
            if (likes.Count() < 10)
            {
                countOfNames = likes.Count();
            }
            if (likes.Count() > 10)
            {
                countOfNames = 10;
            }

            for (int i = 0; i < countOfNames; i++)
            {
                names += likes[i].UserNameWho + "\n";
            }          
            return names;
        }

        public string GetDislikesNames(int postId)
        {
            int countOfNames = 0;
            string names = "";
            List<Like> dislikes = _likesRepository.GetListDislikesByPostId(postId);
            if (dislikes.Count() < 10)
            {
                countOfNames = dislikes.Count();
            }
            if (dislikes.Count() > 10)
            {
                countOfNames = 10;
            }

            for (int i = 0; i < countOfNames; i++)
            {
                names += dislikes[i].UserNameWho + "\n";
            }
            return names;
        }

        private UserCommentViewModel FillViewComment(string comment, int id, string userWho, string userTo, string time)
        {
            var postComment = new UserCommentViewModel();
            postComment.UserPostCreator = userWho;
            postComment.Comment = comment;
            postComment.CreatedTime = time;
            postComment.User = userTo;
            postComment.PostId = id;
            return postComment;
        }

        private Comment FillPostComment(string comment, int id, string userWho, string userTo, string time, Post post)
        {
            Comment userComment = new Comment();
            userComment.Post = post;
            userComment.User = userWho;
            userComment.CreatedComment = time;
            userComment.UserComment = comment;
            userComment.UserPostCreator = userTo;
            userComment.PostId = id;
            return userComment;
        }

        private Like FillLikeOrDislike(string userName, int postId, bool getLike, Post userPost)
        {
            Like like = new Like();
            like.Post = userPost;
            like.PostId = postId;
            like.UserLike = getLike;
            like.UserNameWho = userName;
            return like;
        }
    }
}
