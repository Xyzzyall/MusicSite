using MediatR;
using MusicSite.Shared.SharedModels;
using MusicSite.Shared.SharedModels.Anon;

namespace MusicSite.Server.Commands.Releases
{
    public class UpdateReleaseCommand: IRequest
    {
        public UpdateReleaseCommand(ReleaseSharedEditMode release, int id)
        {
            Release = release;
            Id = id;
        }

        public ReleaseSharedEditMode Release { get; set; }
        public int Id { get; set; }
    }
}
