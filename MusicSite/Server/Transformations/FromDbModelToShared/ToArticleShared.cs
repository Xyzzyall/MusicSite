namespace MusicSite.Server.Transformations.FromDbModelToShared
{
    public class ToArticleSharedIndex : Shared.SharedModels.ArticleSharedIndex
    {
        public ToArticleSharedIndex(Models.Article article)
        {
            Title = article.Title;
            Language = article.Language;
            Tags = article.Tags.Select(tag => tag.Name).ToArray();
            ShortText = article.ShortText;
            //CreatedDate = article.CreatedDate,
            UpdatedDate = article.ShowUpdatedDate ? article.UpdatedDate : null;
            PublishDate = article.PublishDate;
            RelatedRelease = article.RelatedRelease is null ? null : new ToReleaseSharedIndex(article.RelatedRelease);
        }
    }

    public class ToArticleSharedDetail: Shared.SharedModels.ArticleSharedDetail
    {
        public ToArticleSharedDetail(Models.Article article)
        {
            Title = article.Title;
            Language = article.Language;
            Tags = article.Tags.Select(tag => tag.Name).ToArray();
            ShortText = article.ShortText;
            Text = article.Text;
            UpdatedDate = article.ShowUpdatedDate ? article.UpdatedDate : null;
            PublishDate = article.PublishDate;
            RelatedRelease = article.RelatedRelease is null ? null : new ToReleaseSharedDetail(article.RelatedRelease);
        }
    }

}
