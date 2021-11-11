using NxtGen.Account.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NxtGen.Account.API.BusinessLogic.Contracts
{
    public interface IAccountService
    {
        bool Register(RegisterRequestViewModel model, string origin);
        void VerifyEmail(string token);
    }
}
