using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Oryx.Utilities.EmailTool
{
    public class EmailHelper
    {
        private readonly SmtpClient client;

        public string FromEmail;

        public EmailHelper(string server, int port, NetworkCredential credential, string email)
        {
            FromEmail = email;
            client = new SmtpClient();
            client = new SmtpClient(server, port);
            client.UseDefaultCredentials = false;
            client.Credentials = credential;
        }

        public void SendEmail(List<string> address, string subject, string body)
        {
            foreach (var item in address)
            {
                SendEmail(item, subject, body);
            }
        }

        public void SendEmail(string addresss, string subject, string body)
        {
            MailMessage mailMessage = new MailMessage(); 
            mailMessage.Bcc.Add("mandyman@impulseeventhk.com"); 
            mailMessage.From = new MailAddress(FromEmail);
            mailMessage.To.Add(addresss);
            mailMessage.Body = body;
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            client.Send(mailMessage);
        }

        public string UseTemplate(string sourceData, Dictionary<string, string> dataMap)
        {
            foreach (var item in dataMap)
            {
                sourceData = sourceData.Replace("{" + item.Key + "}", item.Value);
            }

            return sourceData;
        }
    }
}
