using Climapi.Core.Entities;
using Climapi.DataEF;
using Climapi.DataEF.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DataEF.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGenericRepository<QueryRecord> QueryRecords { get; }

        private readonly CoreDbContext _context;
        private bool disposed = false;

        public UnitOfWork(CoreDbContext context, UserManager<User> userManager)
        {
            _context = context;
            QueryRecords = QueryRecords ?? new GenericRepository<QueryRecord>(_context);
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
