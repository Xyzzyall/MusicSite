using FluentValidation;
using MusicSite.Server.Data.Interfaces;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Validations.Articles
{
    public class ArticleCreateValidator : AbstractValidator<ArticleCreate>
    {
        public ArticleCreateValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(article => article.Title).NotEmpty();
            RuleFor(article => article.Language).NotEmpty();
            RuleFor(article => article.ShortText).NotEmpty();
            RuleFor(article => article.Tags).NotEmpty();
            
            RuleFor(article => article)
                .MustAsync(async (article, cancellation) => await unitOfWork.Releases
                    .ReleaseExistsAsync(
                        article.Language,
                        article.RelatedReleaseCodename ?? string.Empty,
                        cancellation
                    )
                )
                .When(article => article.RelatedReleaseCodename is not null)
                .WithName("RelatedReleaseCodename")
                .WithMessage(article => $"Prompted release with codename '{article.RelatedReleaseCodename}' is not exist.");

            RuleFor(article => article)
                .MustAsync(async (article, cancellation) => !await unitOfWork.Articles
                    .ArticleExistsAsync(article.Language, article.Title, cancellation)
                )
                .WithMessage("Article with given language and title already exists.");
        }
    }
}
