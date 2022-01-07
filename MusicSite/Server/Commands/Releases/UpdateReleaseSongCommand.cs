using MediatR;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Commands.Releases
{
    public class UpdateReleaseSongCommand : IRequest
    {
        public UpdateReleaseSongCommand(ReleaseSongShared song, int releaseId)
        {
            Song = song;
            ReleaseId = releaseId;
        }

        public ReleaseSongShared Song { get; set; }
        public int ReleaseId { get; set; }
    }
}
