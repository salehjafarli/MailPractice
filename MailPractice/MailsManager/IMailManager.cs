using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailPractice.MailsManager
{
    public interface IMailManager
    {
        Task<List<MimeMessage>> Receive();
        Task Send(MimeMessage message);
    }
}
