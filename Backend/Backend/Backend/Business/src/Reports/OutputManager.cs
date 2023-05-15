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

namespace Backend.Business.src.Reports
{
    public class OutputManager
    {
        private string _SMTPusername = "kibbutznet@gmail.com";
        private string _SMTPpassword = "passKibbutz123";
        
        public void sendEmail(string targetEmail, string subject, string body)
        {
            string fromMail = "amitgrumet8@gmail.com";
            string fromPassword = "xqovfzcfdtbnehcc";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(new MailAddress(targetEmail));
            message.Body = $"<html><body> {GenerateHtmlTable(body)} </body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587, 
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }
        
        public static string GenerateHtmlTable(string jsonData)
        {
            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.AppendLine("<table>");
            htmlBuilder.AppendLine("<tr>");

            // Extracting column headers from the first JSON object
            List<Dictionary<string, object>> items = ParseJson(jsonData);
            Dictionary<string, object> firstItem = items.FirstOrDefault();
            if (firstItem != null)
            {
                foreach (string key in firstItem.Keys)
                {
                    htmlBuilder.AppendLine("<th>" + key + "</th>");
                }

                htmlBuilder.AppendLine("</tr>");

                // Parsing and adding data rows
                foreach (var item in items)
                {
                    htmlBuilder.AppendLine("<tr>");
                    foreach (var value in item.Values)
                    {
                        htmlBuilder.AppendLine("<td>" + value + "</td>");
                    }
                    htmlBuilder.AppendLine("</tr>");
                }
            }

            htmlBuilder.AppendLine("</table>");

            return htmlBuilder.ToString();
        }

        // JSON parser for extracting the data
        private static List<Dictionary<string, object>> ParseJson(string jsonData)
        {
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                ReadCommentHandling = JsonCommentHandling.Skip
            };

            var items = new List<Dictionary<string, object>>();
            using (JsonDocument document = JsonDocument.Parse(jsonData))
            {
                if (document.RootElement.ValueKind == JsonValueKind.Object)
                {
                    var item = new Dictionary<string, object>();
                    ProcessJsonObject(document.RootElement, item);
                    items.Add(item);
                }
            }

            return items;
        }

        private static void ProcessJsonObject(JsonElement jsonElement, Dictionary<string, object> item)
        {
            foreach (JsonProperty property in jsonElement.EnumerateObject())
            {
                string key = property.Name;
                JsonElement value = property.Value;

                if (value.ValueKind == JsonValueKind.Object)
                {
                    var nestedItem = new Dictionary<string, object>();
                    ProcessJsonObject(value, nestedItem);
                    item[key] = nestedItem;
                }
                else if (value.ValueKind == JsonValueKind.Array)
                {
                    var array = new List<Dictionary<string, object>>();
                    foreach (JsonElement arrayElement in value.EnumerateArray())
                    {
                        var nestedItem = new Dictionary<string, object>();
                        ProcessJsonObject(arrayElement, nestedItem);
                        array.Add(nestedItem);
                    }
                    item[key] = array;
                }
                else if (value.ValueKind == JsonValueKind.String)
                {
                    item[key] = value.GetString();
                }
                else if (value.ValueKind == JsonValueKind.Number)
                {
                    if (value.TryGetInt32(out int intValue))
                    {
                        item[key] = intValue;
                    }
                    else if (value.TryGetDouble(out double doubleValue))
                    {
                        item[key] = doubleValue;
                    }
                    else if (value.TryGetDecimal(out decimal decimalValue))
                    {
                        item[key] = decimalValue;
                    }
                    else
                    {
                        item[key] = value.ToString();
                    }
                }
                else
                {
                    item[key] = value.ToString();
                }
            }
        }
        
    }
}