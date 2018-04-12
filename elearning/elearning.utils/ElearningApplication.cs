using System;
using System.Configuration;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using CMCommon.Authentication.Implementation;
using CMCommon.Authentication.Infrastructure;
using CMCommon.Emailing.Implementation;
using CMCommon.Emailing.Interfaces;
using CMCommon.Logging.Implementation;
using CMCommon.Logging.Interfaces;
using CMCommon.Model;
using CMCommon.Security.Implementation;
using CMCommon.Security.Interfaces;
using CMCommon.Utils.Implementation;
using CMCommon.Utils.Interfaces;
using elearning.services.Implementation;
using elearning.services.Interfaces;

namespace elearning.utils
{
    public class ElearningApplication : HttpApplication
    {

        public ContainerBuilder GetBuilder(Type type)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(type.Assembly).PropertiesAutowired();

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(type.Assembly);
            builder.RegisterModelBinderProvider();


            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();
            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());
            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            var sessionId = "";
            var connectionString = ConfigurationManager.ConnectionStrings["elearningConn"].ConnectionString;
            var environment = ConfigurationManager.AppSettings["Environment"];
            var cookieDomain = ConfigurationManager.AppSettings["CookieDomain"];
            var cookiePrefix = ConfigurationManager.AppSettings["CookiePrefix"];

            builder.RegisterType<EncryptionService>().As<IEncryptionService>();
            builder.RegisterType<EmailTemplateService>().As<IEmailTemplateService>();
            builder.RegisterType<RandomStringGenerator>().As<IRandomStringGenerator>();

            builder.Register(c => new DbLoggerService(sessionId, connectionString))
                .As<ILoggerService>().InstancePerLifetimeScope();

            builder.Register(c => new OckAuthService(c.Resolve<IEncryptionService>(),
                c.Resolve<ILoggerService>(), cookieDomain, cookiePrefix))
                .As<IOAuthService>().InstancePerLifetimeScope();

            builder.Register(c => new
              UserService(
                  c.Resolve<ILoggerService>(), c.Resolve<IEncryptionService>()
                  )
                ).As<IUserService>().InstancePerLifetimeScope();

            if (environment.Equals(model.Enums.Environment.local.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                var pickupDir = ConfigurationManager.AppSettings["LocalEmailPickupDir"];
                builder.Register(c => new EmailService(pickupDir, c.Resolve<ILoggerService>()))
                    .As<IEmailService>().InstancePerLifetimeScope();
            }
            else
            {
                var smtpSettings = new SmtpSettings
                {
                    SMTPServer = ConfigurationManager.AppSettings["SmtpServer"],
                    UserName = ConfigurationManager.AppSettings["EmailNetworkUsername"],
                    Password = ConfigurationManager.AppSettings["EmailNetworkPassword"] //please encrypt
                };

                builder.Register(c => new EmailService(smtpSettings, c.Resolve<ILoggerService>()))
                    .As<IEmailService>().InstancePerLifetimeScope();
            }

            builder.RegisterType<ArticleCatogoryService>().As<IArticleCatogoryService>();
            builder.RegisterType<ArticleService>().As<IArticleService>();

            return builder;

        }

    }
}
