﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Musicians_Pocket_Knife.Models;

#nullable disable

namespace Musicians_Pocket_Knife.Migrations
{
    [DbContext(typeof(MpkdbContext))]
    partial class MpkdbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Musicians_Pocket_Knife.Models.Dbsong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Apiid")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("APIid");

                    b.Property<string>("Artist")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("OriginalKey")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int?>("PlaylistId")
                        .HasColumnType("int");

                    b.Property<int?>("SongIndex")
                        .HasColumnType("int");

                    b.Property<string>("Tempo")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("TimeSignature")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Title")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TransposedKey")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id")
                        .HasName("PK__DBSongs__3213E83FB0A1AA73");

                    b.HasIndex("PlaylistId");

                    b.ToTable("DBSongs", (string)null);
                });

            modelBuilder.Entity("Musicians_Pocket_Knife.Models.Playlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("LastDateViewed")
                        .HasColumnType("datetime");

                    b.Property<string>("ListTitle")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Playlist__3213E83F93567F1A");

                    b.HasIndex("UserId");

                    b.ToTable("Playlists");
                });

            modelBuilder.Entity("Musicians_Pocket_Knife.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("GoogleId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("GoogleID");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__Users__3213E83F7264FFE3");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Musicians_Pocket_Knife.Models.Dbsong", b =>
                {
                    b.HasOne("Musicians_Pocket_Knife.Models.Playlist", "Playlist")
                        .WithMany("Dbsongs")
                        .HasForeignKey("PlaylistId")
                        .HasConstraintName("FK__DBSongs__Playlis__18EBB532");

                    b.Navigation("Playlist");
                });

            modelBuilder.Entity("Musicians_Pocket_Knife.Models.Playlist", b =>
                {
                    b.HasOne("Musicians_Pocket_Knife.Models.User", "User")
                        .WithMany("Playlists")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Playlists__UserI__160F4887");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Musicians_Pocket_Knife.Models.Playlist", b =>
                {
                    b.Navigation("Dbsongs");
                });

            modelBuilder.Entity("Musicians_Pocket_Knife.Models.User", b =>
                {
                    b.Navigation("Playlists");
                });
#pragma warning restore 612, 618
        }
    }
}
