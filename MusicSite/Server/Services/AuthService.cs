using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MusicSite.Server.Data;
using MusicSite.Server.Models.Service;
using MusicSite.Server.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MusicSite.Server.Services
{
    public record struct AuthResult(string Token, bool Success, ICollection<string> Errors);
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(string secret, CancellationToken cancellationToken);
    }

    public class AuthService : IAuthService
    {
        private readonly MyJwtOptions _myJwtOptions;
        private readonly ILogger<AuthService> _logger;
        private readonly MusicSiteServerContext _context;

        public AuthService(MyJwtOptions myJwtOptions, ILogger<AuthService> logger, MusicSiteServerContext context)
        {
            _myJwtOptions = myJwtOptions;
            _logger = logger;
            _context = context;
        }

        public async Task<AuthResult> LoginAsync(string secret, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Attempt to login with secret={Secret}", secret);
            var query = _context.User.Where(u => u.Secret == secret);
            var user = await query.FirstOrDefaultAsync(cancellationToken);
            if (user is null)
            {
                _logger.LogError("Login fail: secret('{Secret}') is invalid.", secret);
                return new AuthResult(string.Empty, false, new[] { "Secret is invalid." });
            }

            var token_string = BuildAndSerializeToken(user);
            _logger.LogInformation("User '{Name}' successfully authentificated with token={Token}", user.Name, token_string);
            return new AuthResult(token_string, true, Array.Empty<string>());
        }

        private string BuildAndSerializeToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = _myJwtOptions.GetSecretInBytes();
            var tokenDescriptor = new SecurityTokenDescriptor 
            {
                Subject = new ClaimsIdentity( new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("rights", user.Rights)
                }),
                Expires = DateTime.UtcNow.AddHours(_myJwtOptions.HoursExpires),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token =  tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
