using AutoMapper;
using Platypus.Model.Data.BankAccount;
using Platypus.Model.Entity;

namespace Platypus.Model.Mapping {

    public class BankAccountMappingProfile : Profile {

        public BankAccountMappingProfile() {
            CreateMap<BankAccount, BankAccountModel>();
            CreateMap<BankAccountCreateModel, BankAccount>();
        }
    }
}