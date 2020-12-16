using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Platypus.Model.Entity {

    public class User {

        public User() {
            this.UserId = Guid.NewGuid();
            this.CreatedUtc = DateTime.UtcNow;
        }

        [Key]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(255)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(255)]
        public string Lastname { get; set; }

        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        public Guid? PasswordResetToken { get; set; }

        public Guid? EmailVerificationToken { get; set; }

        [Required]
        [StringLength(255)]
        public string Salt { get; set; }

        public bool Deleted { get; set; }

        public bool EmailVerified { get; set; }

        public DateTime CreatedUtc { get; set; }

        public IList<UserToken> UserTokens { get; set; }
    }
}