using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platypus.Model.Data.Security;
using Platypus.Service.Data.Token.Interface;
using System.Threading.Tasks;

namespace Platypus.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class AuthorisationController : ControllerBase {
        private readonly ITokenRefreshService tokenRefreshService;
        private readonly ITokenRequestService tokenRequestService;

        public AuthorisationController(
            ITokenRefreshService tokenRefreshService,
            ITokenRequestService tokenRequestService) {
            this.tokenRefreshService = tokenRefreshService;
            this.tokenRequestService = tokenRequestService;
        }

        [HttpPost("token")]
        [ProducesResponseType(200, Type = typeof(TokenModel))]
        [ProducesResponseType(401)]
        [AllowAnonymous]
        public async Task<IActionResult> Token(TokenRequestModel model) {
            TokenModel result = await tokenRequestService.AuthenticateAsync(model);

            if (result == null || result.AccessToken == null) {
                return Unauthorized();
            }

            return Ok(result);
        }

        [HttpPost("refreshToken")]
        [ProducesResponseType(200, Type = typeof(TokenModel))]
        [ProducesResponseType(401)]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(TokenModel model) {
            TokenModel result = await tokenRefreshService.RefreshAsync(model);

            if (result == null || result.AccessToken == null) {
                return NotFound();
            }

            return Ok(result);
        }
    }
}