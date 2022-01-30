namespace MusicSite.Shared.SharedModels;

public record ArticleShared
(
    int Id,
    string Title,
    string Language,
    List<string> Tags,
    string ShortText,
    string Text,
    DateTime UpdatedDate,
    DateTime CreatedDate,
    DateTime PublishDate,
    bool HideFromIndex,
    ReleaseShared? RelatedRelease,
    bool ShowUpdatedDate
);