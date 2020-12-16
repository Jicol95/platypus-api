using System;
using System.ComponentModel.DataAnnotations;

namespace Platypus.Model.Entity {

    public class UserToken {

        [Key]
        public Guid UserTokenId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; }

        [Required]
        [StringLength(255)]
        public string Token { get; set; }

        [Required]
        public DateTime Expires { get; set; }
    }
}