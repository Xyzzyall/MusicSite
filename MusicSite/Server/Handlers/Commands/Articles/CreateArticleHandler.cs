using MediatR;
using MusicSite.Server.Commands.Articles;
using MusicSite.Server.Commands;
using MusicSite.Server.Data.Interfaces;
using MusicSite.Server.Data.Models;

namespace MusicSite.Server.Handlers.Commands.Articles
{
    public class CreateArticleHandler : IRequestHandler<CreateArticleCommand, IValidatedResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateArticleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IValidatedResponse> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var request_article = request.Article;
            var article_entity = new Article
            {
                Title = request_article.Title,
                Language = request_article.Language,
                Tags = new List<Tag>(),
                Text = request_article.Text ?? string.Empty,
                ShortText = request_article.ShortText,
                ShowUpdatedDate = request_article.ShowUpdatedDate,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                PublishDate = request_article.PublishDate ?? DateTime.Now
            };
            await _unitOfWork.Articles.AssociateTagsAsync(article_entity, request_article.Tags, cancellationToken);
            if (request_article.RelatedReleaseCodename is not null)
            {
                await _unitOfWork.Articles
                    .AssociateReleaseByCodenameAsync(
                        article_entity, request_article.RelatedReleaseCodename,
                        cancellationToken
                    );
            }

            _unitOfWork.Articles.Add(article_entity);
            await _unitOfWork.CompleteAsync(cancellationToken);
            
            return new ValidatedResponse<int>(article_entity.Id);
        }
    }
}
