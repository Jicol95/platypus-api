using Platypus.Model.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Platypus.Model.Data.Transaction {

    public class TransactionCreateModel : IValidatableObject {
        public TransactionType Type { get; set; }

        [Required]
        [StringLength(3)]
        public string Category { get; set; }

        public decimal Amount { get; set; }

        [Required]
        [StringLength(255)]
        public string VendorName { get; set; }

        public DateTime PurchaseDateUtc { get; set; }

        public Guid BankAccountId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            return new List<ValidationResult>();
        }
    }
}