using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data.Models;
using MusicSite.Server.Data.Models.Service;

namespace MusicSite.Server.Data
{
    public class MusicSiteServerContext : DbContext
    {
        public MusicSiteServerContext (DbContextOptions<MusicSiteServerContext> options)
            : base(options)
        {
        }

        public DbSet<Release> Release { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Models.Release.CreateModel(modelBuilder);
            Models.ReleaseSong.CreateModel(modelBuilder);

            Models.Article.CreateModel(modelBuilder);

            Models.Tag.CreateModel(modelBuilder);

            Models.Service.User.CreateModel(modelBuilder);

            Seed(modelBuilder);
        }

        protected void Seed(ModelBuilder modelBuilder)
        {
            var user_entity = modelBuilder.Entity<Models.Service.User>();
            user_entity.HasData(
                new Models.Service.User { Id = -1, Name = "test", Secret = "test", Rights = "test1;test2"}    
            );
        }
    }
}
