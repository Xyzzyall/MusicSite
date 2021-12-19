namespace MusicSite.Server.Transformations.FromDbModelToShared
{
    public class ToReleaseSharedIndex : Shared.SharedModels.ReleaseSharedIndex
    {
        public ToReleaseSharedIndex(Models.Release release)
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

    public class ToReleaseSharedDetail : Shared.SharedModels.ReleaseSharedDetail
    {
        public ToReleaseSharedDetail(Models.Release release)
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
            ).ToArray();
            DurationInSecs = release.ReleaseSongs.Sum(song => song.LengthSecs);
        }

        public class ToReleaseSongSharedIndex : Shared.SharedModels.ReleaseSongShared
        {
            public ToReleaseSongSharedIndex(Models.ReleaseSong song)
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

    public class ToReleaseSongSharedDetail : Shared.SharedModels.ReleaseSongShared
    {

    }
}
