using System.Net;
using System.Net.Mail;

namespace Backend.Business.src.Reports
{
    public class OutputManager
    {
        private string _SMTPusername = "kibbutznet@gmail.com";
        private string _SMTPpassword = "passKibbutz123";
        
        public void sendEmail(string targetEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                UseDefaultCredentials = true,
                Port = 587,
                Credentials = new NetworkCredential(_SMTPusername, _SMTPpassword),
                EnableSsl = true,
            };
            
    
            smtpClient.Send(_SMTPusername, targetEmail, subject, body);
        }
        
        
    }
}