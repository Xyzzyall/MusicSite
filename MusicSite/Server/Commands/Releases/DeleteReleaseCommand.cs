using MediatR;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Commands.Releases
{
    public class DeleteReleaseCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteReleaseCommand(int id)
        {
            Id = id;
        }
    }
}
