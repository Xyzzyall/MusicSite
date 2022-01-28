using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MusicSite.Server.Data;
using MusicSite.Server.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MusicSite.Server.Data.Models.Service;
using MusicSite.Server.Services.Interfaces;

namespace MusicSite.Server.Services
{
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

        public async ValueTask<IAuthService.AuthResult> LoginAsync(string secret, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Attempt to login with secret={Secret}", secret);
            var query = _context.User.Where(u => u.Secret == secret);
            var user = await query.FirstOrDefaultAsync(cancellationToken);
            if (user is null)
            {
                _logger.LogError("Login fail: secret('{Secret}') is invalid", secret);
                return new IAuthService.AuthResult(string.Empty, false, new[] { "Secret is invalid." });
            }

            var token_string = BuildAndSerializeToken(user);
            _logger.LogInformation("User '{Name}' successfully authenticated with token={Token}", user.Name, token_string);
            return new IAuthService.AuthResult(token_string, true, Array.Empty<string>());
        }

        public async ValueTask<IAuthService.AuthResult> RefreshToken(string userName, CancellationToken cancellationToken)
        {
            var user = await _context.User
                .Where(u => u.Name == userName)
                .FirstOrDefaultAsync(cancellationToken);
            
            if (user is null)
            {
                _logger.LogError("RefreshToken for user '{UserName}' was not successful", userName);
                return new IAuthService.AuthResult(string.Empty, false, new[] { "Failed to refresh token. Maybe user was deleted?" });
            }
            
            var token_string = BuildAndSerializeToken(user);
            return new IAuthService.AuthResult(token_string, true, Array.Empty<string>());
        }

        private string BuildAndSerializeToken(User user)
        {
            var token_handler = new JwtSecurityTokenHandler();
            var key = _myJwtOptions.GetSecretInBytes();
            var token_descriptor = new SecurityTokenDescriptor 
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

            var token =  token_handler.CreateToken(token_descriptor);
            return token_handler.WriteToken(token);
        }
    }
}
