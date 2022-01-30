using MediatR;

namespace MusicSite.Server.Queries.Anon.Article
{
    public class ArticleExistsQuery : IRequest<bool>
    {
        public ArticleExistsQuery(string language, string title)
        {
            Language = language;
            Title = title;
        }
        public string Language { get; set; }
        public string Title { get; set; }
    }
}
