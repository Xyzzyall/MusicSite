using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MusicSite.Server.Models
{
    public class Release
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Codename { get; set; }

        [Required, MaxLength(50)]
        public string Language { get; set; }

        [Required, MaxLength(50)]
        public string Status { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string Type { get; set; }

        [Required]
        public DateTime DateRelease { get; set; }

        [Required, MaxLength(50)]
        public string Author { get; set; }

        [MaxLength(200)]
        public string ShortDescription { get; set; }

        [MaxLength(2000)]
        public string? Description { get; set; }

        public ICollection<ReleaseSong> ReleaseSongs { get; set; }

        public ICollection<Article>? RelatedArticles { get; }

        public static void CreateModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Release>()
                .HasIndex(release => new { release.Codename, release.Language })
                .IsUnique();
        }
    }
}
