using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data.Models;

namespace MusicSite.Server.Data.Repositories.ArticlesRepo;

public partial class ArticlesRepository
{
    public Task<List<Article>> GetArticlesAnonPagedAsync(string language, int page, int recordsPerPage,
        CancellationToken cancellationToken,
        List<string>? tags = null)
    {
        return GetArticlesPagedAsync(language, page, recordsPerPage, cancellationToken, tags,
            article => !article.HideFromIndex && article.PublishDate > DateTime.Now 
        );
    }

    public Task<Article?> TryToGetArticleAnonAsync(string language, string title, CancellationToken cancellationToken)
    {
        return Context.Article
            .Where(article => 
                article.Language == language && article.Title == title
                && !article.HideFromIndex && article.PublishDate > DateTime.Now
            )
            .FirstOrDefaultAsync(cancellationToken);
    }
}