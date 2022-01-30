using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data;
using MusicSite.Server.Queries.Anon.Release;

namespace MusicSite.Server.Handlers.Anon.Releases
{
    public class ReleaseExistsHandler : IRequestHandler<ReleaseExitstsQuery, bool>
    {
        private readonly MusicSiteServerContext _context;

        public ReleaseExistsHandler(MusicSiteServerContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ReleaseExitstsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Release
                .AnyAsync(
                    release => release.Codename == request.Codename && release.Language == request.Language, 
                    cancellationToken
                );
        }
    }
}
