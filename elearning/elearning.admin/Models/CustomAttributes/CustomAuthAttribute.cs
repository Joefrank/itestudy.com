using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CMCommon.Authentication.Infrastructure;
using CMCommon.Authentication.Models;
using CMCommon.Logging.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Authentication.Models
{
    public class CustomAdminAuthAttribute : AuthorizeAttribute 
    {
        private ClientUser _user;

        public string AccessDeniedMessage { get; set; }
        public IOAuthService AuthService { get; set; }
        public ILoggerService Logger { get; set; }
        public List<string> RequiredRoles { get; set; }

        private string _rolesTest = string.Empty;

        public CustomAdminAuthAttribute(string roles)
            : base()
        {
            _rolesTest = roles;
            RequiredRoles = new List<string> { roles };
        }

        public CustomAdminAuthAttribute(IEnumerable<string> roles)
            : base()
        {           
            RequiredRoles = roles.ToList();
        }       
      
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            try
            {
                _user = AuthService.GetClientUser();

                if (_user == null || _user.UserId < 1 || !_user.Roles.Any())//user must be authenticated
                    return false;

                foreach (var role in RequiredRoles)
                {
                    if (_user.IsInRole(role))
                        return true;
                }

                return false;
            }
            catch(Exception ex)
            {
                Logger.LogItem("Authentication failed: " + ex.Message);
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (_user == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }
            else
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    var urlHelper = new UrlHelper(filterContext.RequestContext);
                    filterContext.Result = new JsonResult
                    {
                        Data = new
                        {
                            Error = "NotAuthorized",
                            LogOnUrl = urlHelper.Action("AccessDenied", "Error")
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                } 

                filterContext.Result = new RedirectToRouteResult(new
                RouteValueDictionary(new { controller = "Error", action = "AccessDenied", ModelError = AccessDeniedMessage }));
            }
        }
    }
}
