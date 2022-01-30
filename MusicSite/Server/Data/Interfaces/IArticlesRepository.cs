using System.Linq.Expressions;
using MusicSite.Server.Data.Models;

namespace MusicSite.Server.Data.Interfaces
{
    public interface IArticlesRepository : IRepository<Article>
    {
        Task AssociateTagsAsync(Article article, List<string> tags, CancellationToken cancellationToken);

        Task AssociateReleaseByCodenameAsync(Article article, string releaseCodename, CancellationToken cancellationToken);
        
        Task<List<Article>> GetArticlesPagedAsync(string language, int page, int recordsPerPage,
            CancellationToken cancellationToken, List<string>? tags = null,
            Expression<Func<Article, bool>>? additionalPredicate = null);

        Task<Article?> TryToGetArticleAsync(string language, string title, CancellationToken cancellationToken);

        Task<bool> ArticleExistsAsync(string language, string title, CancellationToken cancellationToken);

        Task<List<Article>> GetArticlesAnonPagedAsync(string language, int page, int recordsPerPage,
            CancellationToken cancellationToken, List<string>? tags = null);
        
        Task<Article?> TryToGetArticleAnonAsync(string language, string title, CancellationToken cancellationToken);
    }
}
