using MediatR;
using MusicSite.Server.Data.Interfaces;
using MusicSite.Server.Queries.Anon.Article;
using MusicSite.Server.Transformations.FromDbModelToShared;
using MusicSite.Shared.SharedModels;
using MusicSite.Shared.SharedModels.Anon;

namespace MusicSite.Server.Handlers.Anon.Articles
{
    public class IndexArticlesByTagsHandler : IRequestHandler<IndexArticlesByTagsQuery, ArticleSharedIndex[]>
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexArticlesByTagsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ArticleSharedIndex[]> Handle(IndexArticlesByTagsQuery request, CancellationToken cancellationToken)
        {
            var articles = await _unitOfWork.Articles
                .GetArticlesAnonPagedAsync(
                    request.Language, request.Page, request.RecordsPerPage,
                    cancellationToken,
                    request.Tags
                );

            return articles.Select(
                article => article.ToArticleSharedIndex()
            ).ToArray();
        }
    }
}
