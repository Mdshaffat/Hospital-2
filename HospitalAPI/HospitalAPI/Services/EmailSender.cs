using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HospitalAPI.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            EmailSettings = emailSettings.Value;
        }
        public EmailSettings EmailSettings { get; }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            Execute(email, subject, htmlMessage).Wait();
            return Task.FromResult(0);
        }

        public async Task Execute(string email, string subject, string htmlMessage)
        {
            try
            {
                var From = new MailAddress("shaffat.mail@gmail.com", "shaffat.mail@gmail.com");
                MailMessage mail = new()
                {
                    From = From

                };

                mail.To.Add(new MailAddress(email));
                mail.Subject = subject;
                mail.Body = htmlMessage;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using SmtpClient smtp = new("smtp.gmail.com");
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential("shaffat.mail@gmail.com", "Gm@ilGm@il");
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                await smtp.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
