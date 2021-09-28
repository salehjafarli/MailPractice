using MailPractice.MailsManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailPractice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        public IMailManager Manager { get; set; }

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMailManager Manager)
        {
            _logger = logger;
            this.Manager = Manager;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            //MimeMessage message = new MimeMessage();
            //message.From.Add(new MailboxAddress("Testing", "salehtesting1@gmail.com"));
            //message.To.Add(new MailboxAddress("Testing", "salehtesting2@gmail.com"));
            //message.Subject = "test";
            //message.Body = new TextPart("plain") { Text = "hello world" };
            //Manager.Send(message);



            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
