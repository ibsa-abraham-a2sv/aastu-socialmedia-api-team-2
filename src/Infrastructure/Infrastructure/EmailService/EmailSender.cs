using System.Net;
using System.Net.Mail;
using Application.Contracts.Email;
using Application.Models.Email;
using Microsoft.Extensions.Options;

namespace Infrastructure.EmailService
{
    public class EmailSender : IEmailSender
    {
        public EmailSettings EmailSettings { get; }
        public SmtpClient SmtpClient { get; }

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            EmailSettings = emailSettings.Value;
             SmtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(EmailSettings.FromAddress, EmailSettings.FromPassword),
                EnableSsl = true,
            };
        }
        public Task<bool> SendEmail(EmailMessage email)
        {

            var message = new MailMessage()
            {
                From = new MailAddress(EmailSettings.FromAddress),
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = true,
                // make the to value of the user email change string to email coll
                To = { new MailAddress(email.To) }

            };
    
            // SmtpClient.Send(EmailSettings.FromAddress, email.To, "Confirm Email", "Success is expected");
            // SmtpClient.Send(message);
            return SendEmailAsync(message);
        }


        private async Task<bool> SendEmailAsync(MailMessage mail)
        {
            try
            {
                await SmtpClient.SendMailAsync(mail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
