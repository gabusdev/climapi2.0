using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Climapi.DataEF.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        //Task<T> GetAsync(object id, params Expression<Func<T, object>>[] includes);
        //Task<ICollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task InsertAsync(T t);
        Task InsertRangeAsync(List<T> t);
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
        Task<T?> GetAsync(Expression<Func<T, bool>> match, params Expression<Func<T, object>>[] includes);
        Task<ICollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes);
        Task<ICollection<T>> GetAllOrderedAsync(Expression<Func<T, object>> orderBy, bool desc,
            params Expression<Func<T, object>>[] includes);
        Task<ICollection<T>> GetAllOrderedAsync(Expression<Func<T, bool>> predicate,
            Expression<Func<T, object>> orderBy, bool desc,
            params Expression<Func<T, object>>[] includes);
        //IQueryable<T> GetAllQuery(Expression<Func<T, bool>> predicate,
        //    Expression<Func<T, object>> orderBy, bool desc,
        //    params Expression<Func<T, object>>[] includes);

        void Update(T t);
        void Delete(object id);
        void DeleteRange(IEnumerable<T> entities);
        Task<bool> Exists(Expression<Func<T, bool>> match);
    }
}