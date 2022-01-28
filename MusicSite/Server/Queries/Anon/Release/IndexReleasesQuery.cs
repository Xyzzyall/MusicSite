using MediatR;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Queries.Anon.Release
{
    public class IndexReleasesQuery : IRequest<List<ReleaseSharedIndex>>
    {
        public IndexReleasesQuery(string language, int page, int recordsPerPage)
        {
            Language = language;
            Page = page;
            RecordsPerPage = recordsPerPage;
        }

        public string Language { get; set; }
        public int Page { get; set; }
        public int RecordsPerPage { get; set; }
    }
}
