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
    public class MailConsumer : IJob
    {
        public ILogger<MailProducer> Logger { get; }
        public IMailManager MailManager { get; set; }
        public MailConsumer(ILogger<MailProducer> Logger, IMailManager MailManager)
        {
            this.Logger = Logger;
            this.MailManager = MailManager;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var x = MailManager.Receive().Result;
            foreach (var item in x)
            {
                Logger.LogInformation($"From:{item.From.First()}\n Subject:{item.Subject} Message:{item.Body}");
            }
            return Task.CompletedTask;
        }
    }
}
