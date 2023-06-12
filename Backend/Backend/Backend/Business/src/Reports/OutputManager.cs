using System;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Backend.Access;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Order = Backend.Business.src.Utils.Order;
using Purchase = Backend.Business.src.Utils.Purchase;

namespace Backend.Business.src.Reports
{
    public class OutputManager
    {
        private string _SMTPusername = "kibbutznet@gmail.com";
        private string _SMTPpassword = "passKibbutz123";

        public void sendEmail(string targetEmail, string subject, List<Utils.Purchase> purchases, List<Order> orders)
        {
            string fromMail = "amitgrumet8@gmail.com";
            string fromPassword = "xqovfzcfdtbnehcc";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(new MailAddress(targetEmail));
            message.Body = $"<html><body> {GenerateHtmlTable(purchases, orders)} </body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }

        public void sendSMS(string target, List<Utils.Purchase> purchases, List<Order> orders)
        {
            // Find your Account SID and Auth Token at twilio.com/console
            // and set the environment variables. See http://twil.io/secure
            string accountSid = "ACd1c2bdd9b975cced26d57bad1daa9fcf";
            string authToken = "1c60f98b764be825f0389bb1ca5cefc5";
            string twilioPhoneNumber = "+13613092791";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: GenerateSMSReport(purchases, orders),
                from: new Twilio.Types.PhoneNumber(twilioPhoneNumber),
                to: new Twilio.Types.PhoneNumber(target)
            );

            Console.WriteLine(message.Sid);
        }
        
        public string GenerateSMSReport(List<Purchase> purchases, List<Order> orders)
        {
            string body = "\nPurchases:\n";

            foreach (Purchase purchase in purchases)
            {
                body += $"Member: {purchase.memberId}, Cost: {purchase.cost}, Description: {purchase.description}, Date: {purchase.date:yyyy-MM-dd HH:mm:ss}\n\n";
            }
            
            body += "\n\nOrders:\n";
            
            foreach (Order order in orders)
            {
                body +=  $"Member: {order.memberId}, Date: {order.date:yyyy-MM-dd HH:mm:ss}, Status: {order.status}, Active: {order.active.ToString()}\n\n";
            }

            return body;
        }
        

        public string GenerateHtmlTable(List<Purchase> purchases, List<Order> orders)
        {
            string body = @" <html>
                                <head>
                                  <style>
                                    table {
                                      border-collapse: collapse;
                                      width: 100%;
                                    }

                                    th, td {
                                      text-align: left;
                                      padding: 8px;
                                      border-bottom: 1px solid #ddd;
                                    }

                                    th {
                                      background-color: #f2f2f2;
                                    }
                                  </style>
                                </head>
                                <body>
                                  <h2>Orders</h2>
                                  <table>
                                    <tr>
                                      <th>Order ID</th>
                                      <th>Time</th>
                                      <th>Status</th>
                                      <th>member Name</th>
                                      <th>Budget Number</th>
                                      <th>Active</th>
                                    </tr>";

            foreach (Order order in orders)
            {
                body += $@"<tr>
                              <th>{order.orderId}</th>
                              <th>{order.date:yyyy-MM-dd HH:mm:ss}</th>
                              <th>{order.status}</th>
                              <th>{order.memberName}</th>
                              <th>{order.memberId}</th>
                              <th>{order.active.ToString()}</th>
                            </tr>
                            ";
            }

            body += @"</table>
                        <h2>Purchases</h2>
                        <table>
                        <tr>
                            <th>Purchase Id</th>
                            <th>Member Id</th>
                            <th>Store Id</th>
                            <th>Cost</th>
                            <th>Description</th>
                            <th>Time</th>
                        </tr>";
            
            foreach (Purchase purchase in purchases)
            {
                body += $@"<tr>
                              <td>{purchase.purchaseId}</td>
                              <td>{purchase.memberId}</td>
                              <td>{purchase.storeId}</td>
                              <td>{purchase.cost}</td>
                              <td>{purchase.description}</td>
                              <td>{purchase.date:yyyy-MM-dd HH:mm:ss}</td>
                            </tr>
                            ";
            }
            
            return body + "</table></body>";
        }
    }
}