using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSite.Shared.SharedModels
{
    public class Release
    {
        public string Codename { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime DateRelease { get; set; }
        public string Author { get; set; }
        public string ShortDescription { get; set; }
        public string? Description { get; set; }

        public ICollection<ReleaseSong>? Songs { get; set; }

        public int DurationInSecs { get; set; }
    }

    public class ReleaseSong
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int LengthSecs { get; set; }
        public string? Lyrics { get; set; }
        public bool IsInstrumental { get; set; }
    }
}
