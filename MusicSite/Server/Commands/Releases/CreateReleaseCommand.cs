using MediatR;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Commands.Releases
{
    public class CreateReleaseCommand : IRequest<int>
    {
        public ReleaseSharedEditMode release { get; set; }

        public CreateReleaseCommand(ReleaseSharedEditMode release)
        {
            this.release = release;
        }
    }
}
