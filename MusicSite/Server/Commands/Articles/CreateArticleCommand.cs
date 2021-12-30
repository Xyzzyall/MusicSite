using MediatR;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Commands.Articles
{
    public class CreateArticleCommand : IRequest<int>
    {
        public ArticleSharedEditMode article { get; set; }

        public CreateArticleCommand(ArticleSharedEditMode article)
        {
            this.article = article;
        }
    }
}
