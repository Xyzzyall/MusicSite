namespace MusicSite.Server.Data.Interfaces;

public interface IUnitOfWork
{
    IArticlesRepository Articles { get;  }
    ITagsRepository Tags { get; }
    IReleasesRepository Releases { get; }
    
    Task<int> CompleteAsync(CancellationToken cancellationToken);
}