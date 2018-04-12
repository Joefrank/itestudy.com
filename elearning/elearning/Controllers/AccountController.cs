using CMCommon.Authentication.Models;
using CMCommon.Model;
using elearning.model.DataModels;
using elearning.model.Enums;
using elearning.model.ViewModels;
using elearning.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace elearning.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View(new RegisterVm());
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
                /*Profiles = userstruct.UserProfiles.ConvertAll(x => x.id.ToString(CultureInfo.InvariantCulture)),*/
                Roles = user.Roles.Split(',').ToList(),
                FullName = user.Firstname + " " + user.Lastname,
                CookieDuration = new TimeSpan(0, 0, LoginDuration, 0)
            };

            AuthService.PersistClientUser(clientUser);
            return Redirect("~/myaccount");
        }

        [HttpPost]
        public ActionResult Register(RegisterVm model)
        {
            model.IsDirty = true;

            if (!model.AcceptedTerms)
            {
                ModelState.AddModelError("Terms not accepted", "You need to accept our terms and conditions before proceeding.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }            

            //check that user with similar email doesn't exist
            var existingUser = UserService.GetUserByEmail(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("User already exist", "Sorry but the email you have provided already exist on our system. Request a password reminder to use it.");
                return View(model);
            }

            //set the activation link before passing it to create user
            model.ActivationLink = RandomStringGenerator.GenerateRandomString(15);
            model.Password = EncryptionService.Encrypt(model.Password);

           //try creating user
            var user = UserService.CreateUser(model);

            //if user is null, we cannot continue.
            if(user == null)
            {
                ModelState.AddModelError("Add user Failed", "Sorry but your details could not be saved. Please contact admin for this issue.");
                return View(model);
            }

            NotifyNewUserOfRegistration(user);
            NotifyNewUserOfRegistrationAdmin(user);

            var userModel = new ActivatedUserVm
            {
                Active = false,
                Firstname = (user != null) ? user.Firstname : "",
                Surname = (user != null) ? user.Lastname : "",
                Signature = CompanySignatureHtml
            };

            return View("RegisterWelcome", userModel);
        }       

        public ActionResult Activate(string id)
        {
            //do activation and redirect to activation confirmation page.
            var code = string.IsNullOrEmpty(id) ? id : Server.UrlDecode(id);
            var userToActivate = UserService.GetUserByActivationCode(code);
            var accountActivated = false;

            if(userToActivate != null)
            {
                accountActivated = UserService.ActivateUser(userToActivate.Id);
            }

            var model = new ActivatedUserVm
            {
                Active = (userToActivate != null) && accountActivated,
                Firstname = (userToActivate != null)? userToActivate.Firstname : "",
                Surname = (userToActivate != null)? userToActivate.Lastname : "",
                Signature = CompanySignatureHtml
            };

            return View(model);
        }

        #region notifications

        public void NotifyNewUserOfRegistration(User user)
        {
            var template = EmailTemplateService.GetTemplate(EmailTemplateType.NewUserRegistration);

            var newUserDico = new Dictionary<string, string>();
            newUserDico.Add("[¬*Firstname*¬]", user.Firstname);
            newUserDico.Add("[¬*ActivationLink*¬]", BaseUrl + "/account/activate/" + user.ActivationLink);
            newUserDico.Add("[¬*Signature*¬]", CompanySignature);

            var emailContent = template.BodyHtml.ReplaceValues(newUserDico);

            var emailStruct = new EmailStruct
            {
                ReceiverEmail = user.Email,
                ReceiverName = user.Firstname,
                SenderEmail = AdminEmail,
                SenderName = SiteName,
                EmailTitle = template.Subject,
                EmailBody = emailContent
            };

            EmailService.SendEmailHTML(emailStruct);
        }

        public void NotifyNewUserOfRegistrationAdmin(User user)
        {
            var template = EmailTemplateService.GetTemplate(EmailTemplateType.NewUserRegistrationAdmin);

            var newUserDico = new Dictionary<string, string>();

            newUserDico.Add("[¬*Firstname*¬]", user.Firstname);
            newUserDico.Add("[¬*Surname*¬]", user.Lastname);
            newUserDico.Add("[¬*Email*¬]", user.Email);
            newUserDico.Add("[¬*UserId*¬]", user.Id.ToString());
            newUserDico.Add("[¬*UserDetailsLink*¬]", BaseAdminUrl + "/user/" + user.Id.ToString());
            newUserDico.Add("[¬*Signature*¬]", CompanySignature);

            var emailContent = template.BodyHtml.ReplaceValues(newUserDico);

            var emailStruct = new EmailStruct
            {
                ReceiverEmail = AdminEmail,
                ReceiverName = SiteName,
                SenderEmail = AdminEmail,
                SenderName = SiteName,
                EmailTitle = template.Subject,
                EmailBody = emailContent
            };

            EmailService.SendEmailHTML(emailStruct);
        }

        public void NotifyNewUserOfActivation(User user)
        {
            var template = EmailTemplateService.GetTemplate(EmailTemplateType.NewUserActivation);

            var newUserDico = new Dictionary<string, string>();
            newUserDico.Add("[¬*Firstname*¬]", user.Firstname);
            newUserDico.Add("[¬*MembershipLink*¬]", BaseUrl + "/pricing");
            newUserDico.Add("[¬*Signature*¬]", CompanySignature);

            var emailContent = template.BodyHtml.ReplaceValues(newUserDico);

            var emailStruct = new EmailStruct
            {
                ReceiverEmail = user.Email,
                ReceiverName = user.Firstname,
                SenderEmail = AdminEmail,
                SenderName = SiteName,
                EmailTitle = template.Subject,
                EmailBody = emailContent
            };

            EmailService.SendEmailHTML(emailStruct);
        }

        public void NotifyNewUserOfActivationAdmin(User user)
        {
            var template = EmailTemplateService.GetTemplate(EmailTemplateType.NewUserActivationAdmin);

            var newUserDico = new Dictionary<string, string>();
            newUserDico.Add("[¬*Firstname*¬]", user.Firstname);
            newUserDico.Add("[¬*UserDetailsLink*¬]", BaseAdminUrl + "/user/" + user.Id.ToString());
            newUserDico.Add("[¬*Signature*¬]", CompanySignature);           
            newUserDico.Add("[¬*Surname*¬]", user.Lastname);
            newUserDico.Add("[¬*Email*¬]", user.Email);
            newUserDico.Add("[¬*UserId*¬]", user.Id.ToString());
            newUserDico.Add("[¬*Title*¬]", user.Title);

            var emailContent = template.BodyHtml.ReplaceValues(newUserDico);

            var emailStruct = new EmailStruct
            {
                ReceiverEmail = AdminEmail,
                ReceiverName = SiteName,
                SenderEmail = AdminEmail,
                SenderName = SiteName,
                EmailTitle = template.Subject,
                EmailBody = emailContent
            };

            EmailService.SendEmailHTML(emailStruct);
        }

        #endregion
    }
}