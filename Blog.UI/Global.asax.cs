using Blog.BLL;
using Blog.BLL.ServiceInterfaces;
using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using System.Web.Routing;

namespace Blog.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IKernel kernal = new StandardKernel();
            DependencyResolver.SetResolver(new MyDependencyResolver(kernal));
            kernal.Bind<IAccountService>().To<AccountLogicService>();
            kernal.Bind<IBlogService>().To<BlogLogicService>();
            kernal.Bind<IAdminService>().To<AdminService>();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }

    public class MyDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public MyDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType, new Parameter[0]);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType, new IParameter[0]);
        }
    }
}
