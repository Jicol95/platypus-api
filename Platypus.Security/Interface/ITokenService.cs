using System;

namespace Platypus.Security.Interface {

    public interface ITokenService {

        string GenerateAccessToken(Guid userId, string firstname, string lastname, Guid? sellerId, Guid? buyerId);

        string GenerateRefreshToken();

        string GetClaimFromAccessToken(string claimType, string accessToken);
    }
}