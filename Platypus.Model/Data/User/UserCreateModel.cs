using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Platypus.Model.Data.User {

    public class UserCreateModel : IValidatableObject {

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            return new List<ValidationResult>();
        }
    }
}