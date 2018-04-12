using Authentication.Models;
using AutoMapper;
using CMCommon.Model;
using elearning.model.DataModels;
using elearning.model.Enums;
using elearning.model.ViewModels;
using elearning.utils;
using System.Collections.Generic;
using System.Web.Mvc;

namespace elearning.Controllers
{
    [CustomMemberAuth(UserRole.Guest)]
    public class MyAccountController : BaseAuthController
    {
        // GET: MyAccount
        public ActionResult Index()
        {
            return View(CurrentUserCl);
        }

        public ActionResult Details()
        {
            var model = Mapper.Map<User, UserDetailsVm>(CurrentUserDb);
           
            return View(model);
        }

        public ActionResult SaveDetails(UserDetailsVm model)
        {
            if (!ModelState.IsValid)
            {
                return View("Details", model);
            }

            if(CurrentUserCl.UserId != model.UserIdentity)
            {
                ModelState.AddModelError("Id mismatch", "Sorry we could not save your details. please contact admin");
                LoggerService.LogItem("IDentity mismatch. could be security issue. UserIdentity " + model.UserIdentity + " Client UserId " + CurrentUserCl.UserId);
                return View("Details", model);
            }

            //update details here.
            var updatedUser = UserService.SaveUserDetails(model);

            NotifyNewUserOfDetailsUpdate(updatedUser);

            return View("Details", model);
        }

        #region notifications

        public void NotifyNewUserOfDetailsUpdate(User user)
        {
            var template = EmailTemplateService.GetTemplate(EmailTemplateType.UserDetailsUpdate);

            var newUserDico = new Dictionary<string, string>();
            newUserDico.Add("[¬*Firstname*¬]", user.Firstname);
            newUserDico.Add("[¬*AccountLink*¬]", BaseUrl + "/myaccount/details");
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

        #endregion

    }
}