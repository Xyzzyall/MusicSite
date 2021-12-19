using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data;
using MusicSite.Server.Queries.Article;
using MusicSite.Server.Transformations.FromDbModelToShared;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Handlers
{
    public class IndexArticlesHandler : IRequestHandler<IndexArticlesQuery, ArticleSharedIndex[]>
    {
        private readonly MusicSiteServerContext _context;

        public IndexArticlesHandler(MusicSiteServerContext contex)
        {
            _context = contex;
        }

        public async Task<ArticleSharedIndex[]> Handle(IndexArticlesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Article
                .Where(article => article.Language == request.Language)
                .Skip(request.Page * request.RecordsPerPage)
                .Take(request.RecordsPerPage);

            var query_result = await query.ToArrayAsync(cancellationToken);

            return query_result.Select(
                article => new ToArticleSharedIndex(article)
            ).ToArray();
        }
    }
}
