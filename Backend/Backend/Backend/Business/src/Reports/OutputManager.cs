using System.Net;
using System.Net.Mail;
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Web; 
using System.Web.UI;

namespace Backend.Business.src.Reports
{
    public class OutputManager
    {
        private string _SMTPusername = "kibbutznet@gmail.com";
        private string _SMTPpassword = "passKibbutz123";
        
        public void sendEmail(string targetEmail, string subject, string body)
        {
            
        }
        
        
    }
}