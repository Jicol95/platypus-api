using AutoMapper;
using Platypus.Model.Data.Transaction;
using Platypus.Model.Entity;
using Platypus.Service.Data.UnitOfWork.Interface;
using System.Threading.Tasks;

namespace Platypus.Service.Data.TransactionServices {

    public class TransactionCreationService : ITransactionCreationService {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TransactionCreationService(
            IUnitOfWork unitOfWork,
            IMapper mapper) {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<TransactionModel> CreateAsync(TransactionCreateModel createModel) {
            Transaction transaction = await unitOfWork.Repository<Transaction>()
                .CreateAsync(mapper.Map<Transaction>(createModel));

            await unitOfWork.SaveChangesAsync();

            return mapper.Map<TransactionModel>(transaction);
        }
    }
}