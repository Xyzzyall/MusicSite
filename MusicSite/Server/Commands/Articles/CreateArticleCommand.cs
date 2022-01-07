using MediatR;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Commands.Articles
{
    public class CreateArticleCommand : IRequest<int>
    {
        public ArticleSharedEditMode Article { get; set; }

        public CreateArticleCommand(ArticleSharedEditMode article)
        {
            this.Article = article;
        }
    }
}
