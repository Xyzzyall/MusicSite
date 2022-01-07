using MediatR;

namespace MusicSite.Server.Queries.Anon.Tags
{
    public class TagsLikeQuery : IRequest<List<string>>
    {
        public string TagPart { get; set; }

        public TagsLikeQuery(string tagPart)
        {
            TagPart = tagPart;
        }
    }
}
