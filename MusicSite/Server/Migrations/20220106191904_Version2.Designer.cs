// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MusicSite.Server.Data;

#nullable disable

namespace MusicSite.Server.Migrations
{
    [DbContext(typeof(MusicSiteServerContext))]
    [Migration("20220106191904_Version2")]
    partial class Version2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ArticleTag", b =>
                {
                    b.Property<int>("ArticlesId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("ArticlesId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("ArticleTag");
                });

            modelBuilder.Entity("MusicSite.Server.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<bool?>("HideFromIndex")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("PublishDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int?>("RelatedReleaseId")
                        .HasColumnType("int");

                    b.Property<string>("ShortText")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("ShowUpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Id");

                    b.HasIndex("RelatedReleaseId");

                    b.HasIndex("Title", "Language")
                        .IsUnique();

                    b.ToTable("Article");
                });

            modelBuilder.Entity("MusicSite.Server.Models.Release", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Codename")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("DateRelease")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<bool?>("IsReleased")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Codename", "Language")
                        .IsUnique();

                    b.ToTable("Release");
                });

            modelBuilder.Entity("MusicSite.Server.Models.ReleaseSong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int>("LengthSecs")
                        .HasColumnType("int");

                    b.Property<string>("Lyrics")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ReleaseId")
                        .HasColumnType("int");

                    b.Property<int>("SongOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReleaseId", "Name")
                        .IsUnique();

                    b.HasIndex("ReleaseId", "SongOrder")
                        .IsUnique();

                    b.ToTable("ReleaseSong");
                });

            modelBuilder.Entity("MusicSite.Server.Models.Service.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Rights")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Secret")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("Secret")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("MusicSite.Server.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("ArticleTag", b =>
                {
                    b.HasOne("MusicSite.Server.Models.Article", null)
                        .WithMany()
                        .HasForeignKey("ArticlesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MusicSite.Server.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MusicSite.Server.Models.Article", b =>
                {
                    b.HasOne("MusicSite.Server.Models.Release", "RelatedRelease")
                        .WithMany("RelatedArticles")
                        .HasForeignKey("RelatedReleaseId");

                    b.Navigation("RelatedRelease");
                });

            modelBuilder.Entity("MusicSite.Server.Models.ReleaseSong", b =>
                {
                    b.HasOne("MusicSite.Server.Models.Release", null)
                        .WithMany("ReleaseSongs")
                        .HasForeignKey("ReleaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MusicSite.Server.Models.Release", b =>
                {
                    b.Navigation("RelatedArticles");

                    b.Navigation("ReleaseSongs");
                });
#pragma warning restore 612, 618
        }
    }
}
