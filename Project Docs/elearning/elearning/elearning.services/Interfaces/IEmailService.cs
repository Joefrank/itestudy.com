using Common.Model;
using elearning.services.Interfaces;

namespace Services.Infrastructure
{
    public interface IEmailService
    {
        void InitSmtp(string smtpServer, string username, string password, ILoggerService logger);

        bool SendEmailHTML(string toAddress, string toName, string fromAddress, string fromName, string subject, string body);

        bool SendEmail(string toAddress, string toName, string fromAddress, string fromName, string subject, string body);

        bool SendMultiPartEmail(string sender, string recipients, string subject, string textBody, string htmlBody);

        bool SendEmailHTML(EmailStruct estruct);

        bool SendEmailWithAttachment(HtmlEmailStruct htmlEmailStruct);

    }
}
