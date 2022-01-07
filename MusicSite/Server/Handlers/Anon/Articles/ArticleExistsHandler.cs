using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data;
using MusicSite.Server.Queries.Anon.Article;
using MusicSite.Server.Transformations.FromDbModelToShared;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Handlers.Anon.Articles
{
    public class ArticleExistsHandler : IRequestHandler<ArticleExistsQuery, bool>
    {
        private readonly MusicSiteServerContext _context;

        public ArticleExistsHandler(MusicSiteServerContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ArticleExistsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Article
                .AnyAsync(article => article.Language == request.Language && article.Title == request.Title);
        }
    }
}
