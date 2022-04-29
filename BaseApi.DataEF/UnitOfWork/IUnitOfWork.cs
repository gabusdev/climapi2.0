using Climapi.Core.Entities;
using Climapi.DataEF.Repository;

namespace DataEF.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<QueryRecord> QueryRecords { get; }

        Task<int> CommitAsync();
    }
}