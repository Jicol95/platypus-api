using Platypus.Repository.Interface;
using System.Threading.Tasks;

namespace Platypus.Service.Data.UnitOfWork.Interface {

    public interface IUnitOfWork {

        void Dispose();

        IDataRepository<TEntity> Repository<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync();
    }
}