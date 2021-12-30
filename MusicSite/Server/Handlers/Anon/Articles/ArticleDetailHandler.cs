using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data;
using MusicSite.Server.Queries.Anon.Article;
using MusicSite.Server.Transformations.FromDbModelToShared;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Handlers.Anon.Articles
{
    public class ArticleDetailHandler : IRequestHandler<ArticleDetailsQuery, ArticleSharedDetail?>
    {
        private readonly MusicSiteServerContext _context;

        public ArticleDetailHandler(MusicSiteServerContext context)
        {
            _context = context;
        }

        public async Task<ArticleSharedDetail?> Handle(ArticleDetailsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Article
                .Where(article => article.Language == request.Language && article.Title == request.Title);
            var query_result = await query.FirstAsync(cancellationToken);
            if (query_result is null) return null;
            return new ToArticleSharedDetail(query_result);
        }
    }
}
