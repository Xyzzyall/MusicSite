using MusicSite.Server.Data.Models;
using MusicSite.Shared.SharedModels.Anon;

namespace MusicSite.Server.Transformations.FromDbModelToShared;

internal static class ToReleaseSharedExtensions
{
    public static ReleaseSharedIndex ToReleaseSharedIndex(this Release release)
    {
        return new ReleaseSharedIndex
        {
            Codename = release.Codename,
            Language = release.Language,
            Name = release.Name,
            Type = release.Type,
            DateRelease = release.DateRelease,
            Author = release.Author,
            ShortDescription = release.ShortDescription,
            DurationInSecs = release.ReleaseSongs.Sum(song => song.LengthSecs) //todo кэшировать это надо
        };
    }

    public static ReleaseSharedDetail ToReleaseSharedDetail(this Release release)
    {
        return new ReleaseSharedDetail
        {
            Codename = release.Codename,
            Language = release.Language,
            Author = release.Author,
            Name = release.Name,
            DateRelease = release.DateRelease,
            Description = release.Description,
            ShortDescription = release.ShortDescription,
            Songs = release.ReleaseSongs.Select(
                song => song.ToReleaseSongShared()
            ).ToList(),
            DurationInSecs = release.ReleaseSongs.Sum(song => song.LengthSecs)
        };
    }

    public static ReleaseSongShared ToReleaseSongShared(this ReleaseSong song)
    {
        return new ReleaseSongShared
        {
            SongOrder = song.SongOrder,
            Name = song.Name,
            Description = song.Description,
            Lyrics = song.Lyrics,
            IsInstrumental = song.Lyrics is null,
            LengthSecs = song.LengthSecs
        };
    }
}