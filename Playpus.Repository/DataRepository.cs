using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Playpus.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Playpus.Repository {

    public class DataRepository<T> : IDataRepository<T> where T : class {
        private readonly DbContext context;
        private readonly DbSet<T> dbSet;

        public DataRepository(DbContext context) {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public async Task<T> GetAsync(Guid id) {
            return await dbSet.FindAsync(id);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate) {
            return await Query().FirstOrDefaultAsync(predicate);
        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate) {
            return await Query().SingleOrDefaultAsync(predicate);
        }

        public IQueryable<T> Query() {
            return dbSet.AsQueryable();
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> predicate) {
            return Query().Where(predicate);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate) {
            return await dbSet.AnyAsync(predicate);
        }

        public async Task<T> CreateAsync(T entity) {
            await dbSet.AddAsync(entity);
            return entity;
        }

        public async Task BulkCreateAsync(IList<T> models) {
            await context.BulkInsertOrUpdateAsync<T>(models);
        }

        public void Update(T entity) {
            dbSet.Update(entity);
        }

        public async Task BulkUpdate(IList<T> models) {
            await context.BulkUpdateAsync<T>(models);
        }

        public async Task DeleteAsync(Guid id) {
            T entity = await dbSet.FindAsync(id);
            dbSet.Remove(entity);
        }

        public async Task DeleteBatchAsync(Expression<Func<T, bool>> predicate) {
            await Query(predicate).BatchDeleteAsync();
        }
    }
}