using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data;
using MusicSite.Server.Data.Interfaces;
using MusicSite.Server.Queries.Anon.Article;
using MusicSite.Server.Transformations.FromDbModelToShared;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Handlers.Anon.Articles
{
    public class IndexArticlesHandler : IRequestHandler<IndexArticlesQuery, ArticleSharedIndex[]>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public IndexArticlesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ArticleSharedIndex[]> Handle(IndexArticlesQuery request, CancellationToken cancellationToken)
        {
            var articles = await _unitOfWork.Articles
                .GetArticlesPagedAsync(request.Language, request.Page, request.RecordsPerPage, cancellationToken);

            return articles.Select(
                article => (ArticleSharedIndex)new ToArticleSharedIndex(article)
            ).ToArray();
        }
    }
}
