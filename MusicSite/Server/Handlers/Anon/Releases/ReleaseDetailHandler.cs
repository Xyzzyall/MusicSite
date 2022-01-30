using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data;
using MusicSite.Server.Queries.Anon.Release;
using MusicSite.Server.Transformations.FromDbModelToShared;
using MusicSite.Shared.SharedModels;
using MusicSite.Shared.SharedModels.Anon;

namespace MusicSite.Server.Handlers.Anon.Releases
{
    public class ReleaseDetailHandler : IRequestHandler<ReleaseDetailQuery, ReleaseSharedDetail?>
    {
        private readonly MusicSiteServerContext _context;

        public ReleaseDetailHandler(MusicSiteServerContext context)
        {
            _context = context;
        }

        public async Task<ReleaseSharedDetail?> Handle(ReleaseDetailQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Release
                .Where(release => release.Codename == request.Codename && release.Language == request.Language);

            var query_result = await query.FirstOrDefaultAsync(cancellationToken);
            return query_result?.ToReleaseSharedDetail();
        }
    }
}
