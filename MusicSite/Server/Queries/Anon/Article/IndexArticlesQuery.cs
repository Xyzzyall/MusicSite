using MediatR;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Queries.Anon.Article
{
    public class IndexArticlesQuery : IRequest<ArticleSharedIndex[]>
    {
        public IndexArticlesQuery(string language, int page, int recordsPerPage)
        {
            Language = language;
            Page = page;
            RecordsPerPage = recordsPerPage;
        }

        public string Language { get; init; }

        public int Page { get; init; }

        public int RecordsPerPage { get; init; }
    }
}
