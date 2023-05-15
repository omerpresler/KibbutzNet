using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Backend.Business.src.Reports
{
    public class OutputManager
    {
        private string _SMTPusername = "kibbutznet@gmail.com";
        private string _SMTPpassword = "passKibbutz123";
        
        public void sendEmail(string targetEmail, string subject, string body)
        {
            string fromMail = "kibutzadamin12@gmail.com";
            string fromPassword = "txxfebllqqzhaiyu";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(new MailAddress(targetEmail));
            message.Body = $"<html><body> {body} </body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587, 
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }
        
        
    }
}