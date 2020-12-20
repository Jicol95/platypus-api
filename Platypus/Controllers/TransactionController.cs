using Microsoft.AspNetCore.Mvc;
using Platypus.Model.Data.Transaction;
using Platypus.Service.Data.TransactionServices;
using System.Threading.Tasks;

namespace Platypus.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase {
        private readonly ITransactionCreationService transactionCreationService;

        public TransactionController(ITransactionCreationService transactionCreationService) {
            this.transactionCreationService = transactionCreationService;
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