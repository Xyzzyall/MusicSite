using MusicSite.Server.Data.Models;
using MusicSite.Shared.SharedModels.Anon;

namespace MusicSite.Server.Transformations.FromDbModelToShared;

internal static class ToArticleSharedExtensions
{
    public static ArticleSharedIndex ToArticleSharedIndex(this Article article)
    {
        return new ArticleSharedIndex
        {
            Title = article.Title,
            Language = article.Language,
            Tags = article.Tags.Select(tag => tag.Name).ToList(),
            ShortText = article.ShortText,
            //CreatedDate = article.CreatedDate,
            UpdatedDate = article.ShowUpdatedDate ? article.UpdatedDate : null,
            PublishDate = article.PublishDate,
            RelatedRelease = article.RelatedRelease?.ToReleaseSharedIndex()
        };
    }

    public static ArticleSharedDetail ToArticleSharedDetail(this Article article)
    {
        return new ArticleSharedDetail
        {
            Title = article.Title,
            Language = article.Language,
            Tags = article.Tags.Select(tag => tag.Name).ToList(),
            ShortText = article.ShortText,
            Text = article.Text,
            UpdatedDate = article.ShowUpdatedDate ? article.UpdatedDate : null,
            PublishDate = article.PublishDate,
            RelatedRelease = article.RelatedRelease?.ToReleaseSharedDetail()
        };
    }
}