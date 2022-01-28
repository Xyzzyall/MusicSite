using MusicSite.Server.Data.Models;

namespace MusicSite.Server.Data.Interfaces;

public interface IReleasesRepository : IRepository<Release>
{
    Task<bool> ReleaseExistsAsync(string language, string codename, CancellationToken cancellationToken);
}