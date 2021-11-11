using Microsoft.EntityFrameworkCore.Storage;
using NxtGen.Account.API.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NxtGen.Account.API.Data.UnitOfWork
{
    public class DbTransaction : ITransaction
    {
        private readonly IDbContextTransaction _efTransaction;

        public DbTransaction(IDbContextTransaction efTransaction)
        {
            _efTransaction = efTransaction;
        }

        public void Commit()
        {
            _efTransaction.Commit();
        }

        public void Dispose()
        {
            _efTransaction.Dispose();
        }

        public void Rollback()
        {
            _efTransaction.Rollback();
        }
    }
}
