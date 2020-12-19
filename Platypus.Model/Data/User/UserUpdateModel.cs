using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Platypus.Model.Data.User {

    public class UserUpdateModel : IValidatableObject {

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            return new List<ValidationResult>();
        }
    }
}