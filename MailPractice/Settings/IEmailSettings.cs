using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailPractice.Settings
{
    public interface IEmailSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string Pop3Server { get; set; }
        public int Pop3Port { get; set; }
        public string Pop3Username { get; set; }
        public string Pop3Password { get; set; }
    }
}
