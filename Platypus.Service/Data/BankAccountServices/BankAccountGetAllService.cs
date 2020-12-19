using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Platypus.Model.Data.BankAccount;
using Platypus.Model.Entity;
using Platypus.Service.Data.BankAccountServices.Interface;
using Platypus.Service.Data.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platypus.Service.Data.BankAccountServices {

    public class BankAccountGetAllService : IBankAccountGetAllService {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public BankAccountGetAllService(
            IUnitOfWork unitOfWork,
            IMapper mapper) {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IList<BankAccountModel>> GetAsync(Guid userId) {
            IList<BankAccountModel> bankAccounts = await unitOfWork.Repository<BankAccount>()
                .Query(row => row.UserId == userId)
                .Select(row => mapper.Map<BankAccountModel>(row))
                .ToListAsync();

            return bankAccounts;
        }
    }
}