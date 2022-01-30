using MediatR;
using MusicSite.Server.Commands.Releases;
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
