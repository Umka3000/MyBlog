using Blog.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.ServiceInterfaces
{
    public interface IAccountService
    {
        void UserOut();
        void RegistrationUser(RegisterViewModel viewModel);
        void UserLogin(LoginViewModel model);
    }
}
