namespace MusicSite.Shared.SharedModels;

public record ReleaseSongCreate
(
    string Name,
    string? Description,
    int LengthSecs,
    string? Lyrics,
    bool IsInstrumental
);