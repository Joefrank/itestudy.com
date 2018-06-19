using System;
using System.Net;
using System.Net.Mime;
using System.IO;
using System.Net.Mail;
using CMCommon.Model;
using CMCommon.Emailing.Interfaces;
using CMCommon.Logging.Interfaces;

namespace CMCommon.Emailing.Implementation
{
    /// <summary>
    /// Generic emailservice needs a logger service
    /// </summary>
    public class EmailService : IEmailService
    {
        public string signature = "";
        public string html_signature = "";
        bool bSMTPInitialized = false;

        NetworkCredential SMTPUserInfo = null;
        SmtpClient emailClient = null;
        ILoggerService _logger;        
                
        public EmailService(SmtpSettings settings, ILoggerService logger)
        {
            SMTPUserInfo = new NetworkCredential(settings.UserName, settings.Password);
            emailClient = new SmtpClient(settings.SMTPServer);
            emailClient.UseDefaultCredentials = false;
            emailClient.Credentials = SMTPUserInfo;
            bSMTPInitialized = true;
            _logger = logger;
        }

        public EmailService(string pickupDirectiroy, ILoggerService logger)
        {            
            emailClient = new SmtpClient();
            emailClient.PickupDirectoryLocation = pickupDirectiroy;
            emailClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            bSMTPInitialized = true;
            _logger = logger;
        }

        public bool SendEmailHTML(string toAddress, string toName, string fromAddress, string fromName, string subject, string body)
        {
            try
            {
                if (!bSMTPInitialized) throw new Exception("Error: SMTP Not initialized");
                MailMessage message = new MailMessage(fromAddress, toAddress, subject, body);
                message.IsBodyHtml = true;
                emailClient.UseDefaultCredentials = false;
                emailClient.Credentials = SMTPUserInfo;
                emailClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogItem("EmailService SendEmailHTML() To " + toAddress + " - email send failed: " + ex.Message);
                return false;
            }
        }

        public bool SendEmail(string toAddress, string toName, string fromAddress, string fromName, string subject, string body)
        {
            try
            {
                if (!bSMTPInitialized) throw new Exception("Error: SMTP Not initialized");
                MailMessage message = new MailMessage(fromAddress, toAddress, subject, body);
                emailClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogItem("EmailService SendEmail() To " + toAddress + " - email send failed: " + ex.Message);
                return false;
            }
        }

        public bool SendMultiPartEmail(string sender, string recipients, string subject, string textBody, string htmlBody)
        {
            try
            {
                if (!bSMTPInitialized) throw new Exception("Error: SMTP Not initialized");
                // Initialize the message with the plain text body:
                MailMessage msg = new MailMessage(sender, recipients, subject, textBody);
                // Convert the html body to a memory stream:
                Byte[] bytes = System.Text.Encoding.UTF8.GetBytes(htmlBody);
                MemoryStream htmlStream = new MemoryStream(bytes);
                // Add the HTML body to the message:
                AlternateView htmlView = new AlternateView(htmlStream, MediaTypeNames.Text.Html);
                msg.AlternateViews.Add(htmlView);
                // Ship it!
                emailClient.Send(msg);
                htmlView.Dispose();
                htmlStream.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogItem("EmailService SendMultiPartEmail() To " + recipients + " - email send failed: " + ex.Message);
                return false;
            }
        }

        public bool SendEmailHTML(EmailStruct estruct)
        {
            try
            {
                if (!bSMTPInitialized) throw new Exception("Error: SMTP Not initialized");
                MailMessage message = new MailMessage(estruct.SenderEmail, estruct.ReceiverEmail, estruct.EmailTitle, estruct.EmailBody.Replace(Environment.NewLine, "<br/>"));
                message.IsBodyHtml = true;
                emailClient.UseDefaultCredentials = false;
                emailClient.Credentials = SMTPUserInfo;
                emailClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogItem("EmailService SendEmailHTML 2() To - email send failed: " + ex.Message);
                return false;
            }
        }

        public bool SendEmail(EmailStruct estruct)
        {
            try
            {
                if (!bSMTPInitialized) throw new Exception("Error: SMTP Not initialized");
                MailMessage message = new MailMessage(estruct.SenderEmail, estruct.ReceiverEmail, estruct.EmailTitle, estruct.EmailBody);
                emailClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogItem("EmailService SendEmail() To - email send failed: " + ex.Message);
                return false;
            }
        }

        public bool SendMultiPartEmail(HtmlEmailStruct htmlEmailStruct)
        {
            try
            {
                if (!bSMTPInitialized) throw new Exception("Error: SMTP Not initialized");
                // Initialize the message with the plain text body:
                MailMessage msg = new MailMessage(htmlEmailStruct.SenderEmail, htmlEmailStruct.ReceiverEmail, htmlEmailStruct.EmailTitle, htmlEmailStruct.EmailBody);
                // Convert the html body to a memory stream:
                Byte[] bytes = System.Text.Encoding.UTF8.GetBytes(htmlEmailStruct.HTMLBody);
                MemoryStream htmlStream = new MemoryStream(bytes);
                // Add the HTML body to the message:
                AlternateView htmlView = new AlternateView(htmlStream, MediaTypeNames.Text.Html);
                msg.AlternateViews.Add(htmlView);
                // Ship it!
                emailClient.Send(msg);
                htmlView.Dispose();
                htmlStream.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogItem("EmailService SendMultiPartEmail() To  - email send failed: " + ex.Message);
                return false;
            }
        }

        public bool SendEmailWithAttachment(HtmlEmailStruct htmlEmailStruct)
        {
            try
            {
                if (!bSMTPInitialized) throw new Exception("Error: SMTP Not initialized");
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(htmlEmailStruct.SenderEmail);
                mail.To.Add(htmlEmailStruct.ReceiverEmail);
                mail.Subject = htmlEmailStruct.EmailTitle;
                mail.Body = htmlEmailStruct.EmailBody;

                if (!string.IsNullOrEmpty(htmlEmailStruct.AttachmentPath))
                {
                    var attachment = new System.Net.Mail.Attachment(htmlEmailStruct.AttachmentPath);

                    mail.Attachments.Add(attachment);
                }
                emailClient.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogItem("EmailService SendEmailWithAttachment() To  - email send failed: " + ex.Message);
                return false;
            }

        }
    }
}
