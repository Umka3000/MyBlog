using Blog.DAL;
using Blog.Entities.Model;
using Blog.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.ServiceInterfaces
{
    public interface IBlogService
    {
        void DeletePostById(int id, string name, bool role);
        UserCommentViewModel AddUserComment(string comment, int id, string userWho, string userTo);
        List<UserPostViewModel> GetAllPosts();
        UserPostViewModel GetPost(int id);
        List<UserCommentViewModel> PostComments(int id);
        List<UserPostViewModel> GetUserPosts(string name);
        UserPostViewModel SaveUserPostChanges(string info, int id);
        int GetCommentsCount(int id);
        int AddUserLike(string userName, int posId);
        void CreateUserPost(UserPostViewModel post);
        int AddUserDislike(string userName, int postId);
        int PostLikes(int postId);
        int PostDislikes(int postId);
        bool CheckUserLike(string userName, int postId);
        string GetLikesNames(int postId);
        string GetDislikesNames(int postId);



    }
}
