using Platypus.Model.Data.BankAccount;
using System;
using System.Threading.Tasks;

namespace Platypus.Service.Data.BankAccountServices.Interface {

    public interface IBankAccountCreationService {

        Task<BankAccountModel> CreateAsync(Guid userId, BankAccountCreateModel createModel);
    }
}