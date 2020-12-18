using AutoMapper;
using Platypus.Model.Data.User;
using Platypus.Model.Entity;

namespace Platypus.Model.Mapping {

    public class UserMappingProfile : Profile {

        public UserMappingProfile() {
            CreateMap<User, UserModel>();
            CreateMap<UserCreateModel, User>();
        }
    }
}