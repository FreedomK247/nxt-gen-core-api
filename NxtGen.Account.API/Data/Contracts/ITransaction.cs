using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NxtGen.Account.API.Data.Contracts
{
    public interface ITransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
