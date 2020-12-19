using Platypus.Model.Data.Security;
using System.Threading.Tasks;

namespace Platypus.Service.Data.TokenServices.Interface {

    public interface ITokenRefreshService {

        Task<TokenModel> RefreshAsync(TokenModel model);
    }
}