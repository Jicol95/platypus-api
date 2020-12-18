using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Playpus.Repository.Interface {

    public interface IDataRepository<T> where T : class {

        Task BulkCreateAsync(IList<T> models);

        Task BulkUpdate(IList<T> models);

        Task<T> CreateAsync(T entity);

        Task DeleteAsync(Guid id);

        Task DeleteBatchAsync(Expression<Func<T, bool>> predicate);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetAsync(Guid id);

        IQueryable<T> Query();

        IQueryable<T> Query(Expression<Func<T, bool>> predicate);

        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        void Update(T entity);
    }
}