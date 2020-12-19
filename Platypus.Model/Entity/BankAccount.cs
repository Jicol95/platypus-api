using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Platypus.Model.Entity {

    public class BankAccount {

        public BankAccount() {
            BankAccountId = Guid.NewGuid();
        }

        [Key]
        public Guid BankAccountId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}