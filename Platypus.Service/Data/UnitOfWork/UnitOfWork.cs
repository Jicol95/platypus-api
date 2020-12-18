using Platypus.Data.Context;
using Platypus.Repository;
using Platypus.Repository.Interface;
using Platypus.Service.Data.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platypus.Service.Data.UnitOfWork {

    public class UnitOfWork : IUnitOfWork {
        private bool disposed;
        private readonly DataContext context;
        private IDictionary<Type, object> repositories;

        public UnitOfWork(DataContext context) {
            this.context = context;
        }

        public IDataRepository<TEntity> Repository<TEntity>() where TEntity : class {
            if (context.Set<TEntity>() == null) {
                throw new NotSupportedException($"{typeof(TEntity)} not a valid DbSet");
            }

            if (repositories == null) {
                repositories = new Dictionary<Type, object>();
            }

            Type type = typeof(TEntity);
            if (!repositories.ContainsKey(type)) {
                repositories[type] = new DataRepository<TEntity>(context);
            }

            return (IDataRepository<TEntity>)repositories[type];
        }

        public async Task<int> SaveChangesAsync() {
            return await context.SaveChangesAsync();
        }

        public void Dispose() {
            if (disposed) {
                return;
            }

            disposed = true;
            context.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}