using AutoMapper;
using Platypus.Model.Data.User;
using Platypus.Model.Entity;
using Platypus.Security.Interface;
using Platypus.Service.Data.UnitOfWork.Interface;
using Platypus.Service.Data.UserServices.Interface;
using System.Threading.Tasks;

namespace Platypus.Service.Data.UserServices {

    public class UserCreationService : IUserCreationService {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHashService hashService;

        public UserCreationService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHashService hashService) {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.hashService = hashService;
        }

        public async Task<UserModel> CreateAsync(UserCreateModel createModel) {
            User user = await unitOfWork.Repository<User>().CreateAsync(mapper.Map<User>(createModel));

            hashService.GetHashAndSaltString(createModel.Password.ToString(), out string password, out string salt);

            user.Password = password;
            user.Salt = salt;

            await unitOfWork.SaveChangesAsync();

            //TODO send welcome/verification email

            return mapper.Map<UserModel>(user);
        }
    }
}