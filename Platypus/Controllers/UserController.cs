using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platypus.Model.Data.User;
using Platypus.Service.Data.UserServices.Interface;
using System.Threading.Tasks;

namespace Platypus.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly IUserCreationService userCreationService;

        public UserController(IUserCreationService userCreationService) {
            this.userCreationService = userCreationService;
        }

        [HttpPost("register")]
        [ProducesResponseType(200, Type = typeof(UserModel))]
        [ProducesResponseType(401)]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]UserCreateModel model) {
            UserModel user = await userCreationService.CreateAsync(model);
            return Ok(user);
        }
    }
}