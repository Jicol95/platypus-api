using AutoMapper;
using Platypus.Model.Data.User;
using Platypus.Model.Entity;
using Platypus.Service.Data.UnitOfWork.Interface;
using Platypus.Service.Data.UserServices.Interface;
using System;
using System.Threading.Tasks;

namespace Platypus.Service.Data.UserServices {

    public class UserUpdateService : IUserUpdateService {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UserUpdateService(
            IMapper mapper,
            IUnitOfWork unitOfWork) {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<UserModel> UpdateAsync(Guid userId, UserUpdateModel model) {
            var user = await unitOfWork.Repository<User>().GetAsync(userId);
            user = mapper.Map(model, user);
            unitOfWork.Repository<User>().Update(user);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<UserModel>(user);
        }
    }
}