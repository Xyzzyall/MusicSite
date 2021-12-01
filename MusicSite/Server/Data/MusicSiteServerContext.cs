using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Models;

namespace MusicSite.Server.Data
{
    public class MusicSiteServerContext : DbContext
    {
        public MusicSiteServerContext (DbContextOptions<MusicSiteServerContext> options)
            : base(options)
        {
        }

        public DbSet<MusicSite.Server.Models.Release> Release { get; set; }
        public DbSet<MusicSite.Server.Models.Article> Article { get; set; }
        public DbSet<MusicSite.Server.Models.Tag> Tag { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Models.Release.CreateModel(modelBuilder);
            Models.ReleaseSong.CreateModel(modelBuilder);

            Models.Article.CreateModel(modelBuilder);

            Models.Tag.CreateModel(modelBuilder);
            //ListOfVal.CreateModel(modelBuilder);

            //Image.CreateModel(modelBuilder);
        }


    }
}
