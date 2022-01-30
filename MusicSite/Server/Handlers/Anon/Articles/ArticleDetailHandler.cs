using MediatR;
using MusicSite.Server.Data.Interfaces;
using MusicSite.Server.Queries.Anon.Article;
using MusicSite.Server.Transformations.FromDbModelToShared;
using MusicSite.Shared.SharedModels;
using MusicSite.Shared.SharedModels.Anon;

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
                .TryToGetArticleAnonAsync(request.Language, request.Title, cancellationToken);
            return article_entity?.ToArticleSharedDetail();
        }
    }
}
