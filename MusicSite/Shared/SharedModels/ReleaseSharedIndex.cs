using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSite.Shared.SharedModels
{
    public record ReleaseSharedIndex
    {
        public string Codename { get; init; }
        public string Language { get; init; }
        public string Name { get; init; }
        public string Type { get; init; }
        public DateTime DateRelease { get; init; }
        public string Author { get; init; }
        public string ShortDescription { get; init; }
        public int DurationInSecs { get; init; }
        public int SongCount { get; init; }
        public bool IsReleased { get; set; }
    }

    public record ReleaseSharedDetail : ReleaseSharedIndex
    {
        public string? Description { get; init; }
        public List<ReleaseSongShared> Songs { get; init; }
    }

    public record ReleaseSongShared
    {
        public int SongOrder { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public int LengthSecs { get; init; }
        public string? Lyrics { get; init; }
        public bool IsInstrumental { get; init; }
    }

    public record ReleaseSharedEditMode : ReleaseSharedDetail
    {
        public int Id { get; init; }
    }
}
