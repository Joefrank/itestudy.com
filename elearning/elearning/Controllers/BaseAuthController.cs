

using Authentication.Models;
using CMCommon.Authentication.Models;
using elearning.model.DataModels;
using elearning.model.Enums;

namespace elearning.Controllers
{
    [CustomMemberAuth(UserRole.Guest)]
    public class BaseAuthController : BaseController
    {
        private User _dbUser = null;

        public ClientUser CurrentUserCl => AuthService.GetClientUser();

        public User CurrentUserDb => _dbUser ?? (_dbUser = UserService.GetUserById(CurrentUserCl.UserId));
    }
}