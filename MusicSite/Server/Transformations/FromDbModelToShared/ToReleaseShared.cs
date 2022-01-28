using MusicSite.Server.Data.Models;

namespace MusicSite.Server.Transformations.FromDbModelToShared
{
    public record ToReleaseSharedIndex : Shared.SharedModels.ReleaseSharedIndex
    {
        public ToReleaseSharedIndex(Release release)
        {
            Codename = release.Codename;
            Language = release.Language;
            Name = release.Name;
            Type = release.Type;
            DateRelease = release.DateRelease;
            Author = release.Author;
            ShortDescription = release.ShortDescription;
            DurationInSecs = release.ReleaseSongs.Sum(song => song.LengthSecs);
        }
    }

    public record ToReleaseSharedDetail : Shared.SharedModels.ReleaseSharedDetail
    {
        public ToReleaseSharedDetail(Release release)
        {
            Codename = release.Codename;
            Language = release.Language;
            Author = release.Author;
            Name = release.Name;
            DateRelease = release.DateRelease;
            Description = release.Description;
            ShortDescription = release.ShortDescription;
            Description = release.Description;
            Songs = release.ReleaseSongs.Select(
                song => new ToReleaseSongSharedIndex(song)
            ).ToList<Shared.SharedModels.ReleaseSongShared>();
            DurationInSecs = release.ReleaseSongs.Sum(song => song.LengthSecs);
        }

        public record ToReleaseSongSharedIndex : Shared.SharedModels.ReleaseSongShared
        {
            public ToReleaseSongSharedIndex(ReleaseSong song)
            {
                SongOrder = song.SongOrder;
                Name = song.Name;
                Description = song.Description;
                Lyrics = song.Lyrics;
                IsInstrumental = song.Lyrics is null;
                LengthSecs = song.LengthSecs;
            }
        }
    }

    public record ToReleaseSongSharedDetail : Shared.SharedModels.ReleaseSongShared
    {
        //todo
    }
}
