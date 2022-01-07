using MediatR;

namespace MusicSite.Server.Queries.Anon.Release
{
    public class ReleaseExitstsQuery : IRequest<bool>
    {
        public ReleaseExitstsQuery(string language, string codename)
        {
            Language = language;
            Codename = codename;
        }

        public string Language { get; set; }
        public string Codename { get; set; }
    }
}
