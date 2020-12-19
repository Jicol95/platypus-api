using Platypus.Model.Data.Security;
using System.Threading.Tasks;

namespace Platypus.Service.Data.TokenServices.Interface {

    public interface ITokenRequestService {

        Task<TokenModel> AuthenticateAsync(TokenRequestModel model);
    }
}