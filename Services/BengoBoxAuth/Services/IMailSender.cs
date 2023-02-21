using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BengoBoxAuth.Services
{
    public interface IMailSender
    {
        void SendEmail(Message message);
    }
}
