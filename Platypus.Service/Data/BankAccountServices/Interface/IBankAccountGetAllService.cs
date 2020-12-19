using Platypus.Model.Data.BankAccount;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platypus.Service.Data.BankAccountServices.Interface {

    public interface IBankAccountGetAllService {

        Task<IList<BankAccountModel>> GetAsync(Guid userId);
    }
}