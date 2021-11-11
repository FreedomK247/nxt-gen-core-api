using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NxtGen.Account.API.ViewModels
{
    public class EmailMessageViewModel
    {
        public List<string> To { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public IFormFileCollection Attachments { get; set; }

        [JsonConstructor]
        public EmailMessageViewModel() { }

        public EmailMessageViewModel(IEnumerable<string> to, string subject, string content, IFormFileCollection attachments)
        {
            //To = new List<MailboxAddress>();
            To = new List<string>();

            //To.AddRange(to.Select(x => new MailboxAddress(x)));
            To.AddRange(to);
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
    }
}
