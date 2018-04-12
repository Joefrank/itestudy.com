using CMCommon.Authentication.Infrastructure;
using CMCommon.Authentication.Models;
using elearning.model.ViewModels;
using elearning.services.Interfaces;
using System;
using System.Linq;
using System.Web.Mvc;

namespace elearning.admin.Controllers
{
    public class AccountController : Controller
    {
        public IUserService UserService { get; set; }
        public IOAuthService AuthService { get; set; }

        public ActionResult Login()
        {
            if (AuthService.IsUserLoggedIn())
                return Redirect("~/");

            return View();
        }       

        [HttpPost]
        public ActionResult Login(LoginVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = UserService.ValidateUser(model);
              
            if(user == null)
            {
                model.LoginFailed = true;
                return View(model);
            }

            var LoginDuration = 30;//put in web config

            //set cookies for login here.
            var clientUser = new ClientUser
            {
                UserName = user.Email,
                UserId = user.Identity,
                Roles = user.Roles.Split(',').ToList(),
                FullName = user.Firstname + " " + user.Lastname,
                CookieDuration = new TimeSpan(0, 0, LoginDuration, 0)
            };

            AuthService.PersistClientUser(clientUser);
            return Redirect("~/");
        }
      
    }
}