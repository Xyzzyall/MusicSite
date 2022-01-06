using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data;
using MusicSite.Server.Queries.Anon.Tags;

namespace MusicSite.Server.Handlers.Anon.Tags
{
    public class TagsLikeHandler : IRequestHandler<TagsLikeQuery, List<string>>
    {
        private readonly MusicSiteServerContext _context;

        public TagsLikeHandler(MusicSiteServerContext context)
        {
            _context = context;
        }

        public async Task<List<string>> Handle(TagsLikeQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Tag.Where(tag => tag.Name.ToUpper().Contains(request.TagPart.ToUpper()));
            var query_result = await query.ToListAsync(cancellationToken);
            return query_result.Select(tag => tag.Name).ToList();
        }
    }
}
