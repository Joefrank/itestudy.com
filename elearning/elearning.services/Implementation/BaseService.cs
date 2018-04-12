
using CMCommon.Logging.Interfaces;
using elearning.services.Interfaces;
using System.Web.Mvc;

namespace elearning.services.Implementation
{
    public class BaseService
    {
        public ILoggerService Logger => DependencyResolver.Current.GetService<ILoggerService>();
        public IUserService UserService => DependencyResolver.Current.GetService<IUserService>();
    }
}
