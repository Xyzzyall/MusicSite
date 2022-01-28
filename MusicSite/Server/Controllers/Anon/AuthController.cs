using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MusicSite.Server.Services;
using MusicSite.Server.Services.Interfaces;
using MusicSite.Shared;

namespace MusicSite.Server.Controllers.Anon
{
    [ApiController, Route(Routing.AuthentificationController)]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("")]
        public async Task<IActionResult> Login(
            string secret,
            CancellationToken cancellationToken
        )
        {
            var auth_result = await _authService.LoginAsync(secret, cancellationToken);
            if (auth_result.Success)
            {
                return Ok(auth_result.Token);
            }
            return BadRequest(auth_result.Errors);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> RefreshToken(CancellationToken cancellationToken)
        {
            var user_name = HttpContext.User.Identity?.Name;
            if (user_name is null)
            {
                return BadRequest("Invalid token structure");
            }

            var auth_result = await _authService.RefreshToken(user_name, cancellationToken);
            if (auth_result.Success)
            {
                return Ok(auth_result.Token);
            }
            return BadRequest(auth_result.Errors);
        }
    }
}
