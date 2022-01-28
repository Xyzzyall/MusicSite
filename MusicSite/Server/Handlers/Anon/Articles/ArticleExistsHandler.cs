using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data;
using MusicSite.Server.Data.Interfaces;
using MusicSite.Server.Queries.Anon.Article;
using MusicSite.Server.Transformations.FromDbModelToShared;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Handlers.Anon.Articles
{
    public class ArticleExistsHandler : IRequestHandler<ArticleExistsQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArticleExistsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ArticleExistsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Articles
                .ArticleExistsAsync(request.Language, request.Title, cancellationToken);
        }
    }
}
