using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data;
using MusicSite.Server.Data.Interfaces;
using MusicSite.Server.Queries.Anon.Article;
using MusicSite.Server.Transformations.FromDbModelToShared;
using MusicSite.Shared.SharedModels;

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
                .GetArticlesPagedAsync(
                    request.Language, request.Page, request.RecordsPerPage,
                    cancellationToken,
                    request.Tags
                );

            return articles.Select(
                article => (ArticleSharedIndex)new ToArticleSharedIndex(article)
            ).ToArray();
        }
    }
}
