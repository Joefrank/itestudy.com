using CMCommon.Logging.Interfaces;
using CMCommon.Model;

namespace CMCommon.Emailing.Interfaces
{
    public interface IEmailService
    {        
        bool SendEmailHTML(string toAddress, string toName, string fromAddress, string fromName, string subject, string body);

        bool SendEmail(string toAddress, string toName, string fromAddress, string fromName, string subject, string body);

        bool SendMultiPartEmail(string sender, string recipients, string subject, string textBody, string htmlBody);

        bool SendEmailHTML(EmailStruct estruct);

        bool SendEmailWithAttachment(HtmlEmailStruct htmlEmailStruct);

    }
}
