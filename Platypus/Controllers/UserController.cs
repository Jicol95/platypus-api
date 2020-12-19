using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platypus.Extensions;
using Platypus.Model.Data.User;
using Platypus.Service.Data.UserServices.Interface;
using System;
using System.Threading.Tasks;

namespace Platypus.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserCreationService userCreationService;
        private readonly IUserUpdateService userUpdateService;

        public UserController(
            IHttpContextAccessor httpContextAccessor,
            IUserCreationService userCreationService,
            IUserUpdateService userUpdateService) {
            this.httpContextAccessor = httpContextAccessor;
            this.userCreationService = userCreationService;
            this.userUpdateService = userUpdateService;
        }

        [HttpPost("register")]
        [ProducesResponseType(200, Type = typeof(UserModel))]
        [ProducesResponseType(401)]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]UserCreateModel model) {
            UserModel user = await userCreationService.CreateAsync(model);
            return Ok(user);
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(UserModel))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateModel model) {
            Guid userId = httpContextAccessor.UserId();
            UserModel user = await userUpdateService.UpdateAsync(userId, model);

            return Ok(User);
        }
    }
}