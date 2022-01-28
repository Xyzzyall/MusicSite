using System.Linq.Expressions;
using MusicSite.Server.Data.Interfaces;
using MusicSite.Server.Data.Models;

namespace MusicSite.Server.Data.Repositories;

public class ReleasesRepository : Repository<Release>, IReleasesRepository
{
    public ReleasesRepository(MusicSiteServerContext context) : base(context)
    {
    }

    public Task<bool> ReleaseExistsAsync(string language, string codename, CancellationToken cancellationToken)
    {
        return ExistsAsync(
            release => release.Codename == codename && release.Language == language,
            cancellationToken
        );
    }

    public override Task<List<Release>> GetAllPagedAsync(int page, int recordsPerPage, CancellationToken cancel)
    {
        throw new NotImplementedException();
    }

    public override Task<List<Release>> FindPagedAsync(Expression<Func<Release, bool>> predicate, int page, int recordsPerPage, CancellationToken cancel)
    {
        throw new NotImplementedException();
    }
}