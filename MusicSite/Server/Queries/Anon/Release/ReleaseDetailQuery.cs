using MediatR;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Queries.Anon.Release
{
    public class ReleaseDetailQuery : IRequest<ReleaseSharedDetail?>
    {
        public ReleaseDetailQuery(string language, string codename)
        {
            Language = language;
            Codename = codename;
        }

        public string Language { get; set; }
        public string Codename { get; set; }
    }
}
