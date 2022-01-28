using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data;
using MusicSite.Server.Data.Interfaces;
using MusicSite.Server.Queries.Anon.Article;
using MusicSite.Server.Transformations.FromDbModelToShared;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Handlers.Anon.Articles
{
    public class ArticleDetailHandler : IRequestHandler<ArticleDetailsQuery, ArticleSharedDetail?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArticleDetailHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ArticleSharedDetail?> Handle(ArticleDetailsQuery request, CancellationToken cancellationToken)
        {
            var article_entity = await _unitOfWork.Articles
                .TryToGetArticleAsync(request.Language, request.Title, cancellationToken);
            return article_entity is null ? null : new ToArticleSharedDetail(article_entity);
        }
    }
}
