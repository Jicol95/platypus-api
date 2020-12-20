using Platypus.Model.Data.Transaction;
using System.Threading.Tasks;

namespace Platypus.Service.Data.TransactionServices {

    public interface ITransactionCreationService {

        Task<TransactionModel> CreateAsync(TransactionCreateModel createModel);
    }
}