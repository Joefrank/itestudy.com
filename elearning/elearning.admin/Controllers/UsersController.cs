using elearning.model.DataModels;
using elearning.model.Enums;
using elearning.model.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using elearning.utils;
using CMCommon.Model;
using AutoMapper;

namespace elearning.admin.Controllers
{
    public class UsersController : BaseAdminController
    {
        // GET: Users
        public ActionResult Index()
        {
            var allUsers = UserService.GetAllUsers();
            return View(allUsers);
        }

        public ActionResult Details(Guid id)
        {
            var user = UserService.GetUserById(id);
            var model = Mapper.Map<User, UserDetailsVm>(user);

            return View(model);
        }

        [HttpPost]
        public ActionResult SaveDetails(UserDetailsVm model)
        {
            if (!ModelState.IsValid)
            {
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
            var template = EmailTemplateService.GetTemplate(EmailTemplateType.UserDetailsUpdateAdmin);//create new template

            var newUserDico = new Dictionary<string, string>();
            newUserDico.Add("[¬*Firstname*¬]", user.Firstname);
            newUserDico.Add("[¬*AccountLink*¬]", BaseUrl + "/myaccount/details");
            newUserDico.Add("[¬*Signature*¬]", CompanySignature);
            newUserDico.Add("[¬*AdminUserName*¬]", CurrentUserCl.FullName);

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