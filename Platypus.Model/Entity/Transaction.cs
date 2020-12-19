using Platypus.Model.Constants;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platypus.Model.Entity {

    public class Transaction {

        public Transaction() {
            TransactionId = Guid.NewGuid();
        }

        public Guid TransactionId { get; set; }

        public TransactionType Type { get; set; }

        [Required]
        [StringLength(3)]
        public string Category { get; set; }

        [Column(TypeName = "decimal(19,4)")]
        public decimal Amount { get; set; }

        public DateTime PurchaseDateUtc { get; set; }

        public Guid BankAccountId { get; set; }

        public BankAccount BankAccount { get; set; }
    }
}