using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MusicSite.Server.Data.Models.Service
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string Secret { get; set; }

        [Required, MaxLength(2000)]
        public string Rights { get; set; }

        public static void CreateModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(user => user.Name)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(user => user.Secret)
                .IsUnique();
        }
    }
}
