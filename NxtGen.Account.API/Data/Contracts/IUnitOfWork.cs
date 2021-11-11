using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NxtGen.Account.API.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ITransaction BeginTransaction();
        void Add<T>(T obj) where T : class;

        void Update<T>(T obj) where T : class;

        void Remove<T>(T obj) where T : class;

        IQueryable<T> Query<T>() where T : class;

        void Commit();

        Task CommitAsync();

        void Attach<T>(T obj) where T : class;
    }
}
