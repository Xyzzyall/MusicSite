using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MusicSite.Server.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Title { get; set; }

        [Required, MaxLength(50)]
        public string Language { get; set; }

        public ICollection<Tag> Tags { get; set; }

        [Required, MaxLength(200)]
        public string ShortText { get; set; }

        [Required, MaxLength(4000)]
        public string Text { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
        public DateTime? PublishDate { get; set; }

        public Release? RelatedRelease { get; set; }


        public static void CreateModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasIndex(article => new { article.Title, article.Language })
                .IsUnique();
        }
    }
}
