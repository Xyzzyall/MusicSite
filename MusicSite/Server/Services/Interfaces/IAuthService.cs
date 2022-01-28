using MusicSite.Server.Data.Models.Service;

namespace MusicSite.Server.Services.Interfaces;

public interface IAuthService
{
    public record struct AuthResult(string Token, bool Success, ICollection<string> Errors);
    
    ValueTask<AuthResult> LoginAsync(string secret, CancellationToken cancellationToken);
    ValueTask<AuthResult> RefreshToken(string userName, CancellationToken cancellationToken);
}