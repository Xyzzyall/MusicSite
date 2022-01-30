using MediatR;
using MusicSite.Shared.SharedModels;
using MusicSite.Shared.SharedModels.Anon;

namespace MusicSite.Server.Queries.Anon.Article
{
    public class IndexArticlesByTagsQuery : IRequest<ArticleSharedIndex[]>
    {
        public IndexArticlesByTagsQuery(string language, List<string> tags, int page, int recordsPerPage)
        {
            Language = language;
            Tags = tags;
            Page = page;
            RecordsPerPage = recordsPerPage;
        }

        public string Language { get; init; }

        public List<string> Tags { get; init; }

        public int Page { get; init; }

        public int RecordsPerPage { get; init; }
    }
}
