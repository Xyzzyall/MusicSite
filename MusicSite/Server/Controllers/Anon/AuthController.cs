using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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
            if (!auth_result.Success) return BadRequest(auth_result.Errors);
            ApplyTokenHeader(auth_result);
            return Ok();
        }

        private void ApplyTokenHeader(IAuthService.AuthResult authResult)
        {
            HttpContext.Response.Headers
                .Add("Authorization", $"Bearer {authResult.Token}");
        }

        [HttpPut("")]
        [Authorize]
        public async Task<IActionResult> RefreshToken(CancellationToken cancellationToken)
        {
            var user_name = HttpContext.User.Identity?.Name;
            var secret = HttpContext.User.Claims
                .FirstOrDefault(c => c.Subject?.Name == "secret")?
                .Value;
            if (user_name is null || secret is null)
            {
                return BadRequest("Invalid token structure");
            }
            
            if (!await _authService.UserExistsAsync(user_name, cancellationToken))
            {
                return BadRequest($"User '{user_name}' was deleted");
            }

            var auth_result = await _authService.LoginAsync(secret, cancellationToken);
            if (auth_result.Success)
            {
                ApplyTokenHeader(auth_result);
                return Ok();
            }
            return BadRequest(auth_result.Errors);
        }
    }
}
