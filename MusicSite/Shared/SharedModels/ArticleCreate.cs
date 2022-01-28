namespace MusicSite.Shared.SharedModels;

public record ArticleCreate
(
    string Title,
    string Language,
    List<string> Tags,
    string ShortText,
    string? Text,
    bool ShowUpdatedDate,
    DateTime? PublishDate, 
    string? RelatedReleaseCodename
);