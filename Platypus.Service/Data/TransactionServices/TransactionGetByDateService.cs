using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Platypus.Model.Data.Transaction;
using Platypus.Model.Entity;
using Platypus.Model.Query;
using Platypus.Service.Data.TransactionServices.Interface;
using Platypus.Service.Data.UnitOfWork.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platypus.Service.Data.TransactionServices {

    public class TransactionGetByDateService : ITransactionGetByDateService {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public TransactionGetByDateService(
            IMapper mapper,
            IUnitOfWork unitOfWork) {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public IList<TransactionModel> Get(TransactionQueryModel model) {
            IEnumerable<Transaction> transactions = unitOfWork.Repository<Transaction>()
                .Query();

            if (model.FromUtc.HasValue && !model.ToUtc.HasValue) {
                transactions = transactions.Where(row => row.PurchaseDateUtc == model.ToUtc);
            } else if (model.FromUtc.HasValue && model.ToUtc.HasValue) {
                transactions = transactions
                    .ToList()
                    .Where(row => row.PurchaseDateUtc.Ticks >= model.FromUtc.Value.Ticks && row.PurchaseDateUtc.Ticks <= model.ToUtc.Value.Ticks);
            }

            if (model.BankAccountId.HasValue) {
                transactions = transactions.Where(row => row.BankAccountId == model.BankAccountId);
            }

            IList<TransactionModel> results = transactions
                .Select(row => mapper.Map<TransactionModel>(row))
                .ToList();

            return results;
        }
    }
}