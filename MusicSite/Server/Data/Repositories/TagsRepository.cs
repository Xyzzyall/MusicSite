using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data.Interfaces;
using MusicSite.Server.Data.Models;

namespace MusicSite.Server.Data.Repositories
{
    public class TagsRepository : Repository<Tag>, ITagsRepository
    {
        public TagsRepository(MusicSiteServerContext context) : base(context)
        {
        }

        public async ValueTask<Tag> GetOrAddTagAsync(string tagName, CancellationToken cancellationToken)
        {
            var query = Context.Tag.Where(tag => tag.Name == tagName);
            if (await query.AnyAsync(cancellationToken))
            {
                return await query.FirstAsync(cancellationToken);
            }
            var created_tag = (
                Context.Tag.Add(
                    new Tag { Name = tagName }
                )
            ).Entity;
            return created_tag;
        }

        public async Task<List<Tag>> GetOrAddTagsAsync(List<string> tags, CancellationToken cancellationToken)
        {
            var tags_db = new List<Tag>();
            foreach (var tag in tags)
            {
                tags_db.Add(await GetOrAddTagAsync(tag, cancellationToken));
            }
            return tags_db;
        }

        public override Task<List<Tag>> GetAllPagedAsync(int page, int recordsPerPage, CancellationToken cancel)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Tag>> FindPagedAsync(Expression<Func<Tag, bool>> predicate, int page,
            int recordsPerPage, CancellationToken cancel, Expression<Func<Tag, bool>>? additionalPredicate = null)

        {
            throw new NotImplementedException();
        }
    }
}
