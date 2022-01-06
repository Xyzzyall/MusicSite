using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data;
using MusicSite.Server.Commands.Articles;
using MusicSite.Server.Transformations.FromDbModelToShared;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Handlers.Commands.Articles
{
    public class CreateArticleHandler : IRequestHandler<CreateArticleCommand, int>
    {
        private readonly MusicSiteServerContext _context;

        public CreateArticleHandler(MusicSiteServerContext context)
        {
            _context = context;
        }

        public Task<int> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            //_context.Article.AddAsync();
            throw new NotImplementedException();
        }
    }
}
