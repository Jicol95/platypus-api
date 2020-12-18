using Platypus.Model.Data.Security;
using System.Threading.Tasks;

namespace Platypus.Service.Data.Token.Interface {

    public interface ITokenRefreshService {

        Task<TokenModel> RefreshAsync(TokenModel model);
    }
}