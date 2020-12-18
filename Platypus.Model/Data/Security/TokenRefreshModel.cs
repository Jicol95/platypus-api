using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Platypus.Model.Data.Security {

    public class TokenRefreshModel : IValidatableObject {

        [Required]
        public string Token { get; set; }

        [Required]
        public string RefreshToken { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            return new List<ValidationResult>();
        }
    }
}