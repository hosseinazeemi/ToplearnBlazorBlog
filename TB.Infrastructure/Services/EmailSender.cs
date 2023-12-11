using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TB.Infrastructure.Interfaces;

namespace TB.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendAsync(string target, string title, string message)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtp = new SmtpClient("mail.example.ir");

            mail.From = new MailAddress("info@example.ir");
            mail.To.Add(target);
            mail.Subject = title;
            mail.Body = message;
            mail.IsBodyHtml = true;

            smtp.Port = 587; // your port
            smtp.Credentials = new NetworkCredential("info@example.ir", "Your Password");
            smtp.EnableSsl = true;

            smtp.Send(mail);

            return Task.CompletedTask;
        }
    }
}
