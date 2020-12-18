using Platypus.Repository.Interface;
using System.Threading.Tasks;

namespace Platypus.Service.Data.Interface {

    public interface IUnitOfWork {

        void Dispose();

        IDataRepository<TEntity> Repository<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync();
    }
}