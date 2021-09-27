
using MailKit.Net.Pop3;
using MailPractice.Settings;
using Microsoft.Extensions.Logging;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
// using System.Net.Mail;
using System.Threading.Tasks;

namespace MailPractice.MailManager
{
    public class MailManager : IMailManager
    {
        public readonly IEmailSettings settings;
        public ILogger<MailManager> Logger { get; set; }
        public MailManager(IEmailSettings settings, ILogger<MailManager> Logger)
        {
            this.settings = settings;
            this.Logger = Logger;
            
        }
        public async Task<List<MimeMessage>> Receive()
        {
            Pop3Client client = new Pop3Client();
            client.Connect(settings.Pop3Server,settings.Pop3Port,true);
            client.Authenticate(settings.Pop3Username,settings.SmtpPassword);
            List<MimeMessage> messages = new List<MimeMessage>(client.Count);
            for (int i = client.Count; i >0 ; i--)
            {
                 messages.Add(await client.GetMessageAsync(i));
            }
            client.Dispose();
            return messages;
        }
        public async Task Send(MimeMessage message) 
        {
            var smtp = new SmtpClient();
            smtp.Connect(settings.SmtpServer, settings.SmtpPort,true);
            smtp.Authenticate(settings.SmtpUsername,settings.SmtpPassword);
            await smtp.SendAsync(message);
            smtp.Dispose();

        }
    }
}
