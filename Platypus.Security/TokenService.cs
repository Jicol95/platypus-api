using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Platypus.Security.Interface;
using Platypus.Security.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Platypus.Security {

    public class TokenService : ITokenService {
        private readonly string key;
        private readonly int tokenExpiryMins;

        public TokenService(IOptions<SecuritySettings> settings) {
            if (settings != null && settings.Value != null) {
                this.key = settings.Value.Key;
                this.tokenExpiryMins = settings.Value.TokenExpiryMins ?? 5;
            }
        }

        public string GenerateAccessToken(Guid userId, string firstname, string lastname, Guid? sellerId, Guid? buyerId) {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(this.key);

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, userId.ToString()),
                new Claim(ClaimTypes.GivenName, firstname),
                new Claim(ClaimTypes.Surname, lastname),
            };

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddMinutes(tokenExpiryMins),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        public string GenerateRefreshToken() {
            byte[] randomNumber = new byte[32];
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public string GetClaimFromAccessToken(string claimType, string accessToken) {
            byte[] key = Encoding.ASCII.GetBytes(this.key);

            TokenValidationParameters tokenValidationParamters = new TokenValidationParameters {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                RequireExpirationTime = false
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            ClaimsPrincipal principal = tokenHandler.ValidateToken(accessToken, tokenValidationParamters, out SecurityToken securityToken);

            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)) {
                throw new SecurityTokenException("Invalid token!");
            }

            string value = principal.FindFirst(claimType)?.Value;

            if (string.IsNullOrEmpty(value)) {
                throw new SecurityTokenException($"Missing claim: {claimType}!");
            }

            return value;
        }
    }
}