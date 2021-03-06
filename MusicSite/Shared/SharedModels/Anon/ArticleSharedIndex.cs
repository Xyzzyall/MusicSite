namespace MusicSite.Shared.SharedModels.Anon
{
    public record ArticleSharedIndex
    {
        public string Title { get; init; }

        public string Language { get; init; }

        public List<string> Tags { get; init; }
        public string ShortText { get; init; }

        public DateTime? UpdatedDate { get; init; }

        public DateTime PublishDate { get; init; }
        public ReleaseSharedIndex? RelatedRelease { get; init; }
    }

    public record ArticleSharedDetail : ArticleSharedIndex
    {
        public string Text { get; init; }
    }

    public record ArticleSharedEditMode : ArticleSharedDetail
    {
        public int Id { get; init; }

        public DateTime CreatedDate { get; init; }

        public DateTime UpdatedDatePrivate { get; init; }

        public bool ShowUpdatedDate { get; set; }
    }
}
