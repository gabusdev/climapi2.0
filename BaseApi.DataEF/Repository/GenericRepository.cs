using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Climapi.DataEF.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly CoreDbContext _context;
        protected readonly DbSet<T> _db;

        public GenericRepository(CoreDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _db = _context.Set<T>();
        }

        public async Task InsertAsync(T t)
        {
            await _db.AddAsync(t);
        }

        public async Task InsertRangeAsync(List<T> t)
        {
            await _db.AddRangeAsync(t);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return predicate is null
                ? await _db.CountAsync()
                : await _db.CountAsync(predicate);
        }

        public async void Delete(object id)
        {
            var entity = await _db.FindAsync(id);
            if (entity != null) 
                _db.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }

        public async Task<ICollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            return await GetAllOrderedAsync(null, null, false, includes);
        }

        public async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return await GetAllOrderedAsync(predicate, null, false, includes);
        }

        public async Task<ICollection<T>> GetAllOrderedAsync(Expression<Func<T, object>> orderBy, bool desc, params Expression<Func<T, object>>[] includes)
        {
            return await GetAllOrderedAsync(null, orderBy, desc, includes);
        }

        public async Task<ICollection<T>> GetAllOrderedAsync(Expression<Func<T, bool>>? predicate,
            Expression<Func<T, object>>? orderBy, bool desc,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = GetAllQuery(predicate, orderBy, desc, includes);
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _db;
            query = AddIncludes(query, includes);
            return await query.SingleOrDefaultAsync(match);
        }

        public void Update(T t)
        {
            _db.Attach(t);
            _context.Entry(t).State = EntityState.Modified;
        }
        
        public async Task<bool> Exists(Expression<Func<T, bool>> match)
        {
            return await _db.FirstOrDefaultAsync(match) is not null;
        }

        private IQueryable<T> AddIncludes(IQueryable<T> query, Expression<Func<T, object>>[] includes)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            query = query.AsSplitQuery();
            return query;
        }

        private IQueryable<T> GetAllQuery(Expression<Func<T, bool>>? predicate, Expression<Func<T, object>>? orderBy, bool desc, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _db;

            if (predicate is not null)
                query = query.Where(predicate);

            query = AddIncludes(query, includes);

            if (orderBy is not null)
                query = !desc
                    ? query.OrderBy(orderBy)
                    : query.OrderByDescending(orderBy);

            return query;
        }
    }
}