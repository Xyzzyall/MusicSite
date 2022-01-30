using MediatR;
using MusicSite.Server.Data.Interfaces;
using MusicSite.Server.Queries.Anon.Article;
using MusicSite.Server.Transformations.FromDbModelToShared;
using MusicSite.Shared.SharedModels;
using MusicSite.Shared.SharedModels.Anon;

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
                .GetArticlesAnonPagedAsync(request.Language, request.Page, request.RecordsPerPage, cancellationToken);

            return articles.Select(
                article => article.ToArticleSharedIndex()
            ).ToArray();
        }
    }
}
