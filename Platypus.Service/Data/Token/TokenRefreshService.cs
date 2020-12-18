using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Platypus.Model.Data.Security;
using Platypus.Model.Data.User;
using Platypus.Model.Entity;
using Platypus.Security.Interface;
using Platypus.Service.Data.Interface;
using Platypus.Service.Data.Token.Interface;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Platypus.Service.Data.Token {

    public class TokenRefreshService : ITokenRefreshService {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ITokenService tokenService;

        public TokenRefreshService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ITokenService tokenService) {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.tokenService = tokenService;
        }

        public async Task<TokenModel> RefreshAsync(TokenModel model) {
            string userIdClaim = tokenService.GetClaimFromAccessToken(ClaimTypes.Name, model.AccessToken);

            if (!Guid.TryParse(userIdClaim, out Guid userId)) {
                return new TokenModel();
            }

            UserToken userToken = await unitOfWork.Repository<UserToken>()
                .FirstOrDefaultAsync(row =>
                    row.UserId == userId &&
                    row.Token == model.RefreshToken &&
                    row.Expires > DateTime.UtcNow);

            if (userToken is null) {
                return new TokenModel();
            }

            User user = await unitOfWork.Repository<User>()
                .SingleOrDefaultAsync(x => x.UserId == userId);

            UserModel userModel = mapper.Map<UserModel>(user);

            return await GenerateToken(userModel);
        }

        private async Task<TokenModel> GenerateToken(UserModel model) {
            User user = await unitOfWork.Repository<User>()
                .Query(row => row.UserId == model.UserId)
                .FirstOrDefaultAsync();

            TokenModel result = new TokenModel {
                AccessToken = tokenService.GenerateAccessToken(model.UserId, user.Firstname, user.Lastname),
                AccessTokenExpires = DateTime.UtcNow.AddMinutes(tokenService.TokenExpiryMins),
                RefreshToken = tokenService.GenerateRefreshToken(),
                UserId = user.UserId,
                EmailAddress = user.EmailAddress,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                CreatedUtc = user.CreatedUtc,
            };

            await unitOfWork.Repository<UserToken>().CreateAsync(new UserToken {
                UserTokenId = Guid.NewGuid(),
                UserId = model.UserId,
                Token = result.RefreshToken,
                Expires = DateTime.UtcNow.AddMinutes(tokenService.RefreshTokenExpiryMins)
            });
            await unitOfWork.SaveChangesAsync();

            await unitOfWork.Repository<UserToken>()
                .DeleteBatchAsync(row => row.Expires <= DateTime.UtcNow);

            return result;
        }
    }
}