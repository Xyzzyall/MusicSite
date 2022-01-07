using MediatR;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Queries.Anon.Article
{
    public class ArticleDetailsQuery : IRequest<ArticleSharedDetail>
    {
        public string Language { get; set; }
        public string Title { get; set; }

        public ArticleDetailsQuery(string language, string title)
        {
            Language = language;
            Title = title;
        }
    }
}
