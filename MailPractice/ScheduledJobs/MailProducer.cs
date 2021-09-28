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
        public Task Execute(IJobExecutionContext context)
        {
            Logger.LogInformation("Job was executed");
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Testing", "salehtesting1@gmail.com"));
            message.To.Add(new MailboxAddress("Testing", "salehtesting2@gmail.com"));
            message.Subject = "test";
            message.Body = new TextPart("plain") { Text = "hello world" + num };
            num += 1;
            Task.Run(async()=> {
                await MailManager.Send(message);
            });
            return Task.CompletedTask;
              
        }
    }
}
