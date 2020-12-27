using Platypus.Model.Data.Transaction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platypus.Service.Data.TransactionServices.Interface {

    public interface ITransactionGetByDateService {

        Task<IList<TransactionModel>> GetAsync(DateTime selectedDate);
    }
}