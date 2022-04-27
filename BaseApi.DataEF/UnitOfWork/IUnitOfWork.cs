using Climapi.Core.Entities;
using Climapi.DataEF.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace DataEF.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<QueryRecord> QueryRecords { get; }
        
        Task<int> CommitAsync();
    }
}