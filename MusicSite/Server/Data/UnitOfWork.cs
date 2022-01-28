using MusicSite.Server.Data.Interfaces;
using MusicSite.Server.Data.Repositories;

namespace MusicSite.Server.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly MusicSiteServerContext _context;

    public ITagsRepository Tags { get; }
    public IArticlesRepository Articles { get; }
    public IReleasesRepository Releases { get; }

    public UnitOfWork(MusicSiteServerContext context)
    {
        _context = context;
        Tags = new TagsRepository(context);
        Articles = new ArticlesRepository(context, Tags);
        Releases = new ReleasesRepository(context);
    }

    public Task<int> CompleteAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}