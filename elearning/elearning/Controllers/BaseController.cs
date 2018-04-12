using CMCommon.Authentication.Infrastructure;
using CMCommon.Emailing.Interfaces;
using CMCommon.Logging.Interfaces;
using CMCommon.Security.Interfaces;
using CMCommon.Utils.Interfaces;
using elearning.services.Interfaces;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace elearning.Controllers
{
    public class BaseController : Controller
    {
        public ILoggerService LoggerService { get; set; }
        public IEmailService EmailService { get; set; }
        public IEmailTemplateService EmailTemplateService { get; set; }
        public IUserService UserService { get; set; }
        public IEncryptionService EncryptionService { get; set; }
        public IRandomStringGenerator RandomStringGenerator { get; set; }
        public IOAuthService AuthService { get; set; }

        public static string CompanySignature => "The Team " + ConfigurationManager.AppSettings["CompanyName"] + Environment.NewLine +
                                          "Email: " + ConfigurationManager.AppSettings["CompanyEmail"] + Environment.NewLine +
                                          "Phone: " + ConfigurationManager.AppSettings["CompanyPhone"];

        public static string CompanySignatureHtml => "The Team " + ConfigurationManager.AppSettings["CompanyName"] + "<br/>" +
                                              "Email: " + ConfigurationManager.AppSettings["CompanyEmail"] + "<br/>" +
                                              "Phone: " + ConfigurationManager.AppSettings["CompanyPhone"];

        public string BaseUrl => ConfigurationManager.AppSettings["BaseUrl"];

        public string BaseAdminUrl => ConfigurationManager.AppSettings["BaseAdminUrl"];

        public string AdminEmail => ConfigurationManager.AppSettings["SiteEmail"];

        public string InfoEmail => ConfigurationManager.AppSettings["CompanyEmail"];

        public string SiteName => ConfigurationManager.AppSettings["SiteAutoMailer"];
    }
}