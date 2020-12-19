using Platypus.Model.Data.User;
using System.Threading.Tasks;

namespace Platypus.Service.Data.UserServices.Interface {

    public interface IUserCreationService {

        Task<UserModel> CreateAsync(UserCreateModel createModel);
    }
}