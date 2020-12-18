using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Platypus.Model.Data.Security {

    public class TokenRequestModel : IValidatableObject {

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            return new List<ValidationResult>();
        }
    }
}