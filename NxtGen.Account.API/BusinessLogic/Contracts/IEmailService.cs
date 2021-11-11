using NxtGen.Account.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NxtGen.Account.API.BusinessLogic.Contracts
{
    public interface IEmailService
    {
        void SendEmail(EmailMessageViewModel message);

        Task SendEmailAsync(EmailMessageViewModel message);
    }
}
