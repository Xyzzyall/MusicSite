using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data;
using MusicSite.Server.Queries.Anon.Release;
using MusicSite.Server.Transformations.FromDbModelToShared;
using MusicSite.Shared.SharedModels;
using MusicSite.Shared.SharedModels.Anon;

namespace MusicSite.Server.Handlers.Anon.Releases
{
    public class IndexReleasesHandler : IRequestHandler<IndexReleasesQuery, List<ReleaseSharedIndex>>
    {
        private readonly MusicSiteServerContext _context;

        public IndexReleasesHandler(MusicSiteServerContext context)
        {
            _context = context;
        }

        public async Task<List<ReleaseSharedIndex>> Handle(IndexReleasesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Release
                .Where(release => release.Language == request.Language)
                .Skip(request.Page * request.RecordsPerPage)
                .Take(request.RecordsPerPage);

            var query_result = await query.ToListAsync(cancellationToken);
            return query_result.Select(
                release => release.ToReleaseSharedIndex()
            ).ToList();
        }
    }
}
