using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data;
using MusicSite.Server.Commands.Articles;
using MusicSite.Server.Transformations.FromDbModelToShared;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Handlers.Commands.Articles
{
    public class UpdateArticleHandler : IRequestHandler<UpdateArticleCommand>
    {
        private readonly MusicSiteServerContext _context;

        public UpdateArticleHandler(MusicSiteServerContext context)
        {
            _context = context;
        }

        public Task<Unit> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
