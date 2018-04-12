

using CMCommon.Authentication.Infrastructure;
using CMCommon.Authentication.Models;
using System.Web.Mvc;

namespace elearning.Helpers
{
    public class AuthHelper
    {
        public static ClientUser GetClientUser()
        {
            var authService = DependencyResolver.Current.GetService<IOAuthService>();
            return authService.GetClientUser();
        }

        public static string GetUserFirstName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                return fullName;

            return fullName.Split()[0];
        }
    }
}