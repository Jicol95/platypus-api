using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Platypus.Model.Data.Transaction;
using Platypus.Model.Entity;
using Platypus.Service.Data.TransactionServices.Interface;
using Platypus.Service.Data.UnitOfWork.Interface;
using System;
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

        public async Task<IList<TransactionModel>> GetAsync(DateTime selectedDate) {
            IList<TransactionModel> transactions = await unitOfWork.Repository<Transaction>()
                .Query(row => row.PurchaseDateUtc.Date == selectedDate.Date)
                .Select(row => mapper.Map<TransactionModel>(row))
                .ToListAsync();

            return transactions;
        }
    }
}