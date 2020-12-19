using Platypus.Model.Constants;
using System;

namespace Platypus.Model.Data.Transaction {

    public class TransactionModel {
        public Guid TransactionId { get; set; }

        public TransactionType Type { get; set; }

        public string Category { get; set; }

        public string CategoryDescription => TransactionCategoryConstant.GetDescription(Category);

        public decimal Amount { get; set; }

        public DateTime PurchaseDateUtc { get; set; }

        public Guid BankAccountId { get; set; }
    }
}