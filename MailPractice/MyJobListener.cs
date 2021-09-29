using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MailPractice
{
    //  Added for some debug purposes
    public class MyJobListener : IJobListener
    {
       
        public ILogger<MyJobListener> Logger { get; set; }
        public MyJobListener(ILogger<MyJobListener> Logger)
        {
            this.Logger = Logger;
        }
        public string Name => "MyJob";

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            Logger.LogInformation($"JobWasExecuted   {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
