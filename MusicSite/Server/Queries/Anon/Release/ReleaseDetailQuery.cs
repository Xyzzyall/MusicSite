using MediatR;
using MusicSite.Shared.SharedModels;
using MusicSite.Shared.SharedModels.Anon;

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
