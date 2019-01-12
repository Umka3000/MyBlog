
using Blog.View;
using Blog.VIewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.ServiceInterfaces
{
    public interface IAdminService
    {
        void DeleteUser(string userName);
        void BlockUser(string userName);
        List<UserViewModel> GetAllUsers();
        void EditUser(EditUserViewModel userView);
        EditUserViewModel SearchUserByEmail(string userEmail);
        void UnblockUser(string userName);
    }
}
