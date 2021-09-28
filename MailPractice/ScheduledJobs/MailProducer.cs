using MailPractice.MailsManager;
using Microsoft.Extensions.Logging;
using MimeKit;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailPractice.ScheduledJobs
{
    [DisallowConcurrentExecution]
    public class MailProducer : IJob
    {
        public static int num = 0;
        public ILogger<MailProducer> Logger { get; }
        public IMailManager MailManager { get; set; }
        public MailProducer(ILogger<MailProducer> Logger, IMailManager MailManager)
        {
            this.Logger = Logger;
            this.MailManager = MailManager;
        }
        private object asd = new object();
        public Task Execute(IJobExecutionContext context)
        {
            num += 1;
            lock (asd)
            {
                Logger.LogInformation($"Job was started {num}   {DateTime.Now}");
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress("Testing", "salehtesting1@gmail.com"));
                message.To.Add(new MailboxAddress("Testing", "salehtesting2@gmail.com")); 
                message.Subject = "test";
                message.Body = new TextPart("plain") { Text = "hello world " + num +" "+ DateTime.Now }; 
                Task.Run(async () => {
                    await MailManager.Send(message);
                });
                return Task.CompletedTask;
            }
            
              
        }
    }
}
