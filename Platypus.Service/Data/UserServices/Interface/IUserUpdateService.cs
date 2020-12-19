using Platypus.Model.Data.User;
using System;
using System.Threading.Tasks;

namespace Platypus.Service.Data.UserServices.Interface {

    public interface IUserUpdateService {

        Task<UserModel> UpdateAsync(Guid userId, UserUpdateModel model);
    }
}