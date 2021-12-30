using MediatR;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Commands.Articles
{
    public class UpdateArticleCommand : IRequest
    {
        public UpdateArticleCommand(int id, ArticleSharedEditMode article)
        {
            Id = id;
            this.article = article;
        }

        public int Id { get; set; }
        public ArticleSharedEditMode article { get; set; }
    }
}
