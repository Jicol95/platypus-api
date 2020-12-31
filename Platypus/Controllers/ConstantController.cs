using Microsoft.AspNetCore.Mvc;
using Platypus.Model.Constants;
using Platypus.Model.Data;
using Platypus.Model.Data.BankAccount;
using System.Linq;

namespace Platypus.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class ConstantContoller : ControllerBase {

        [HttpGet("transactionCategories")]
        [ProducesResponseType(200, Type = typeof(ValueDescriptionModel<string, string>))]
        [ProducesResponseType(401)]
        public IActionResult GetTransactionCategories([FromBody]BankAccountCreateModel model) {
            var constants = TransactionCategoryConstant.ValuesAndDescriptions
                .Select(x => new ValueDescriptionModel<string, string> {
                    Value = x.Key,
                    Description = x.Value
                });

            return Ok(constants);
        }
    }
}