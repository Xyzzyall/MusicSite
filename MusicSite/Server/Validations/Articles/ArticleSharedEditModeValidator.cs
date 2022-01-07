using FluentValidation;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Validations.Articles
{
    public class ArticleSharedEditModeValidator : AbstractValidator<ArticleSharedEditMode>
    {
        public ArticleSharedEditModeValidator()
        {
            RuleFor(article => article.Title).NotEmpty();
            RuleFor(article => article.Language).NotEmpty();
            RuleFor(article => article.ShortText).NotEmpty();
            RuleFor(article => article.Tags).NotEmpty();
            
        }


    }
}
