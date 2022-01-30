using MediatR;
using MusicSite.Server.Data;
using MusicSite.Server.Commands.Articles;

namespace MusicSite.Server.Handlers.Commands.Articles
{
    public class DeleteArticleHandler : IRequestHandler<DeleteArticleCommand>
    {
        private readonly MusicSiteServerContext _context;

        public DeleteArticleHandler(MusicSiteServerContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
