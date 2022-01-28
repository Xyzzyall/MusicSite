using FluentValidation;
using MediatR;
using MusicSite.Server.Data.Interfaces;
using MusicSite.Server.Validations.Articles;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Commands.Articles
{
    public class CreateArticleCommand : IRequest<IValidatedResponse>
    {
        public ArticleCreate Article { get;  }

        public CreateArticleCommand(ArticleCreate article)
        {
            this.Article = article;
        }
    }

    public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
    {
        public CreateArticleCommandValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(command => command.Article).SetValidator(new ArticleCreateValidator(unitOfWork));
        }
    }
}
