using System;

namespace Platypus.Model.Query {

    public class TransactionQueryModel {
        public Guid? BankAccountId { get; set; }

        public DateTime? FromUtc { get; set; }

        public DateTime? ToUtc { get; set; }
    }
}