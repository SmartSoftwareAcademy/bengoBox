using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BengoBoxAuth.Services
{
    public class EmailConfiguration
    {
        public string From { get; set; }
        public string EmailServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
