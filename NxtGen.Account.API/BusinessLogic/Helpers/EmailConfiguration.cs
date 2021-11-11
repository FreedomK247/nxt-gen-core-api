using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NxtGen.Account.API.BusinessLogic.Helpers
{
    public class EmailConfiguration
    {
        public string EmailFrom { get; set; }
        public string SmtpHost { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }
    }
}
