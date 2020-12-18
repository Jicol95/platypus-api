using System;

namespace Platypus.Security.Interface {

    public interface ITokenService {
        int TokenExpiryMins { get; }
        int RefreshTokenExpiryMins { get; }
        int MaxLoginAttempts { get; }

        string GenerateAccessToken(Guid userId, string firstname, string lastname);

        string GenerateRefreshToken();

        string GetClaimFromAccessToken(string claimType, string accessToken);
    }
}