using Platypus.Model.Constants;
using System;

namespace Platypus.Model.Data.Transaction {

    public class TransactionModel : TransactionCreateModel {
        public Guid TransactionId { get; set; }

        public string CategoryDescription => TransactionCategoryConstant.GetDescription(Category);
    }
}