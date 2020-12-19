using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Platypus.Model.Data.BankAccount {

    public class BankAccountCreateModel : IValidatableObject {
        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            return new List<ValidationResult>();
        }
    }
}