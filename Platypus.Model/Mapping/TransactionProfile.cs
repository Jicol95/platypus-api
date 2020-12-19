using AutoMapper;
using Platypus.Model.Data.Transaction;
using Platypus.Model.Entity;

namespace Platypus.Model.Mapping {

    public class TransactionProfile : Profile {

        public TransactionProfile() {
            CreateMap<Transaction, TransactionModel>();
            CreateMap<TransactionCreateModel, Transaction>();
        }
    }
}