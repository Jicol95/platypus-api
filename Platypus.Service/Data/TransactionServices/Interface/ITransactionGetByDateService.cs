using Platypus.Model.Data.Transaction;
using Platypus.Model.Query;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platypus.Service.Data.TransactionServices.Interface {

    public interface ITransactionGetByDateService {

        IList<TransactionModel> Get(TransactionQueryModel model);
    }
}