using MediatR;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data;
using MusicSite.Server.Commands.Releases;
using MusicSite.Server.Transformations.FromDbModelToShared;
using MusicSite.Shared.SharedModels;
using MusicSite.Server.Commands;

namespace MusicSite.Server.Handlers.Commands.Releases
{
    public class CreateReleaseHandler : IRequestHandler<CreateReleaseCommand, IValidatedResponse>
    {
        public Task<IValidatedResponse> Handle(CreateReleaseCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
