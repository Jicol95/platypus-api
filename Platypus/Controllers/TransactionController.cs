using Microsoft.AspNetCore.Mvc;
using Platypus.Model.Data.Transaction;
using Platypus.Model.Query;
using Platypus.Service.Data.TransactionServices;
using Platypus.Service.Data.TransactionServices.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platypus.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase {
        private readonly ITransactionCreationService transactionCreationService;
        private readonly ITransactionGetByDateService transactionGetByDateService;

        public TransactionController(
            ITransactionCreationService transactionCreationService,
            ITransactionGetByDateService transactionGetByDateService) {
            this.transactionCreationService = transactionCreationService;
            this.transactionGetByDateService = transactionGetByDateService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IList<TransactionModel>))]
        [ProducesResponseType(401)]
        public IActionResult GetTransactionByDate([FromQuery] TransactionQueryModel model) {
            IList<TransactionModel> transactions = transactionGetByDateService.Get(model);

            return Ok(transactions);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(TransactionModel))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> CreateTransaction([FromBody]TransactionCreateModel model) {
            TransactionModel transaction = await transactionCreationService.CreateAsync(model);

            return Ok(transaction);
        }
    }
}