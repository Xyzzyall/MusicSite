using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MusicSite.Server.Data.Models
{
    public class ReleaseSong
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ReleaseId { get; set; }

        //[Key, MaxLength(50)]
        //public string Language { get; set; }
        [Required]
        public int SongOrder { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(2000)]
        public string? Description { get; set; }

        [Required]
        public int LengthSecs { get; set; }

        [MaxLength(2000)]
        public string? Lyrics { get; set; }

        public static void CreateModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReleaseSong>()
                .HasIndex(song => new { song.ReleaseId, song.SongOrder })
                .IsUnique();

            modelBuilder.Entity<ReleaseSong>()
                .HasIndex(song => new { song.ReleaseId, song.Name })
                .IsUnique();
        }
    }
}
