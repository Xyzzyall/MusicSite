using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MusicSite.Server.Services;

namespace MusicSite.Server.Controllers.Anon
{
    [ApiController, Route("api/[controller]")]
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
    }
}
