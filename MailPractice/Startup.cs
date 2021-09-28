
using MailPractice.MailsManager;
using MailPractice.ScheduledJobs;
using MailPractice.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MailPractice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var settings = new EmailSettings();
            Configuration.GetSection("EmailSettings").Bind(settings);
            services.AddSingleton<IEmailSettings>(settings);
            services.AddSingleton<IMailManager, MailManager>();

            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                
                var PKey = new JobKey("MailProducerJob");
                var CKey = new JobKey("MailConsumerJob");


                q.AddJob<MailProducer>(opts => opts.WithIdentity(PKey));
                q.AddTrigger(opts => opts
                    .ForJob(PKey) 
                    .WithIdentity("MailProducer-trigger") 
                    .WithCronSchedule(Configuration["CronExpressions:EmailProducerCron"]));



                q.AddJob<MailConsumer>(opts => opts.WithIdentity(CKey));
                q.AddTrigger(opts => opts
                    .ForJob(CKey)
                    .WithIdentity("MailConsumer-trigger")
                    .WithCronSchedule(Configuration["CronExpressions:EmailConsumerCron"]));
            });
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MailPractice", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MailPractice v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
