using AutoMapper;
using Platypus.Model.Data.BankAccount;
using Platypus.Model.Entity;
using Platypus.Service.Data.BankAccountServices.Interface;
using Platypus.Service.Data.UnitOfWork.Interface;
using System;
using System.Threading.Tasks;

namespace Platypus.Service.Data.BankAccountServices {

    public class BankAccountCreationService : IBankAccountCreationService {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public BankAccountCreationService(
            IUnitOfWork unitOfWork,
            IMapper mapper) {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<BankAccountModel> CreateAsync(Guid userId, BankAccountCreateModel createModel) {
            BankAccount bankAccount = await unitOfWork.Repository<BankAccount>()
                .CreateAsync(mapper.Map<BankAccount>(createModel));

            bankAccount.UserId = userId;

            await unitOfWork.SaveChangesAsync();

            return mapper.Map<BankAccountModel>(bankAccount);
        }
    }
}