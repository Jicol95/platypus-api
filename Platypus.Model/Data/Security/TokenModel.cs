using Platypus.Model.Data.User;
using System;

namespace Platypus.Model.Data.Security {

    public class TokenModel : UserModel {
        public string AccessToken { get; set; }

        public DateTimeOffset AccessTokenExpires { get; set; }

        public string RefreshToken { get; set; }
    }
}