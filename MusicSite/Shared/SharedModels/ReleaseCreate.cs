namespace MusicSite.Shared.SharedModels;

public record ReleaseCreate
(
    string Codename,
    string Language,
    string Name,
    string Type,
    DateTime? DateRelease,
    string Author,
    string ShortDescription,
    string? Description,
    bool IsReleased,
    List<ReleaseSongCreate> Songs
);