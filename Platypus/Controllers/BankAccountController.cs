using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platypus.Extensions;
using Platypus.Model.Data.BankAccount;
using Platypus.Service.Data.BankAccountServices.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platypus.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IBankAccountCreationService bankAccountCreationService;
        private readonly IBankAccountGetAllService bankAccountGetAllService;

        public BankAccountController(
            IHttpContextAccessor httpContextAccessor,
            IBankAccountCreationService bankAccountCreationService,
            IBankAccountGetAllService bankAccountGetAllService) {
            this.httpContextAccessor = httpContextAccessor;
            this.bankAccountCreationService = bankAccountCreationService;
            this.bankAccountGetAllService = bankAccountGetAllService;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BankAccountModel))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> CreateBankAccount([FromBody]BankAccountCreateModel model) {
            Guid userId = httpContextAccessor.UserId();
            BankAccountModel bankAccount = await bankAccountCreationService.CreateAsync(userId, model);

            return Ok(bankAccount);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IList<BankAccountModel>))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetBankAccounts() {
            Guid userId = httpContextAccessor.UserId();
            IList<BankAccountModel> bankAccounts = await bankAccountGetAllService.GetAsync(userId);

            return Ok(bankAccounts);
        }
    }
}