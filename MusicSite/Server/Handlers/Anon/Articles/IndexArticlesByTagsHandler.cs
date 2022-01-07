using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data;
using MusicSite.Server.Queries.Anon.Article;
using MusicSite.Server.Transformations.FromDbModelToShared;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Handlers.Anon.Articles
{
    public class IndexArticlesByTagsHandler : IRequestHandler<IndexArticlesByTagsQuery, ArticleSharedIndex[]>
    {
        private readonly MusicSiteServerContext _context;

        public IndexArticlesByTagsHandler(MusicSiteServerContext context)
        {
            _context = context;
        }

        public async Task<ArticleSharedIndex[]> Handle(IndexArticlesByTagsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Article
                .Where(
                    article => article.Language == request.Language
                    && article.Tags.Where(t => request.Tags.Contains(t.Name)).Count() == request.Tags.Count    //todo: there maybe some workaround
                 )
                .Skip(request.Page * request.RecordsPerPage)
                .Take(request.RecordsPerPage);

            var query_result = await query.ToArrayAsync(cancellationToken);

            return query_result.Select(
                article => new ToArticleSharedIndex(article)
            ).ToArray();
        }
    }
}
