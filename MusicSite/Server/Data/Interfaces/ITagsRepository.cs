using MusicSite.Server.Data.Models;

namespace MusicSite.Server.Data.Interfaces
{
    public interface ITagsRepository : IRepository<Tag>
    {
        ValueTask<Tag> GetOrAddTagAsync(string tag, CancellationToken cancellationToken);

        Task<List<Tag>> GetOrAddTagsAsync(List<string> tag, CancellationToken cancellationToken);
    }
}
