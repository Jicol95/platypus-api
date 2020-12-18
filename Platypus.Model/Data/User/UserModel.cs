using System;

namespace Platypus.Model.Data.User {

    public class UserModel {
        public Guid UserId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Fullname => $"{this.Firstname} {this.Lastname}";

        public string EmailAddress { get; set; }

        public DateTime CreatedUtc { get; set; }
    }
}