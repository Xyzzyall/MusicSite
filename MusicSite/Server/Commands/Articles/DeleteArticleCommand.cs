using MediatR;

namespace MusicSite.Server.Commands.Articles
{
    public class DeleteArticleCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteArticleCommand(int id)
        {
            Id = id;
        }
    }
}
