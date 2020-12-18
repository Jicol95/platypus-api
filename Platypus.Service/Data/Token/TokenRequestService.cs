using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Platypus.Model.Data.Security;
using Platypus.Model.Data.User;
using Platypus.Model.Entity;
using Platypus.Security.Interface;
using Platypus.Service.Data.Interface;
using Platypus.Service.Data.Token.Interface;
using System;
using System.Threading.Tasks;

namespace Platypus.Service.Data.Token {

    public class TokenRequestService : ITokenRequestService {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IHashService hashService;
        private readonly ITokenService tokenService;

        public TokenRequestService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IHashService hashService,
            ITokenService tokenService) {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.hashService = hashService;
            this.tokenService = tokenService;
        }

        public async Task<TokenModel> AuthenticateAsync(TokenRequestModel model) {
            User user = await unitOfWork.Repository<User>()
                .Query()
                .SingleOrDefaultAsync(x => x.EmailAddress == model.EmailAddress);

            if (user is null) {
                return new TokenModel();
            }

            UserModel userModel = mapper.Map<UserModel>(user);

            bool validLogin = await VerifyLogin(model.EmailAddress, model.Password);

            if (!validLogin) {
                return new TokenModel();
            }

            TokenModel result = await GenerateToken(userModel);

            return result;
        }

        private async Task<bool> VerifyLogin(string emailAddress, string password) {
            User user = await unitOfWork.Repository<User>()
                .Query()
                .SingleOrDefaultAsync(x => emailAddress == x.EmailAddress);

            if (user is null ||
                !hashService.VerifyHashString(password, user.Password, user.Salt)) {
                return false;
            }

            unitOfWork.Repository<User>().Update(user);
            await unitOfWork.SaveChangesAsync();
            return true;
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