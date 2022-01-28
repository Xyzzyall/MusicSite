using MusicSite.Server.Data.Models;

namespace MusicSite.Server.Transformations.FromDbModelToShared
{
    public record ToArticleSharedIndex : Shared.SharedModels.ArticleSharedIndex
    {
        public ToArticleSharedIndex(Article article)
        {
            Title = article.Title;
            Language = article.Language;
            Tags = article.Tags.Select(tag => tag.Name).ToList();
            ShortText = article.ShortText;
            //CreatedDate = article.CreatedDate,
            UpdatedDate = article.ShowUpdatedDate ? article.UpdatedDate : null;
            PublishDate = article.PublishDate;
            RelatedRelease = article.RelatedRelease is null ? null : new ToReleaseSharedIndex(article.RelatedRelease);
        }
    }

    public record ToArticleSharedDetail: Shared.SharedModels.ArticleSharedDetail
    {
        public ToArticleSharedDetail(Article article)
        {
            Title = article.Title;
            Language = article.Language;
            Tags = article.Tags.Select(tag => tag.Name).ToList();
            ShortText = article.ShortText;
            Text = article.Text;
            UpdatedDate = article.ShowUpdatedDate ? article.UpdatedDate : null;
            PublishDate = article.PublishDate;
            RelatedRelease = article.RelatedRelease is null ? null : new ToReleaseSharedDetail(article.RelatedRelease);
        }
    }

}
