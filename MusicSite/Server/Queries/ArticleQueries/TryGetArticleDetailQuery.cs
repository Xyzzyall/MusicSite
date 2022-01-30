using MediatR;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Queries.ArticleQueries;

public class TryGetArticleDetailQuery : IRequest<ArticleShared?>
{
    public string Title { get; }
    public string Language { get; }

    public TryGetArticleDetailQuery(string title, string language)
    {
        Title = title;
        Language = language;
    }
}