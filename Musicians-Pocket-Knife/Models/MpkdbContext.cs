using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Musicians_Pocket_Knife.Models;

public partial class MpkdbContext : DbContext
{
    public MpkdbContext()
    {
    }

    public MpkdbContext(DbContextOptions<MpkdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dbsong> Dbsongs { get; set; }

    public virtual DbSet<Playlist> Playlists { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dbsong>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DBSongs__3213E83FB0A1AA73");

            entity.ToTable("DBSongs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apiid)
                .HasMaxLength(15)
                .HasColumnName("APIid");
            entity.Property(e => e.Artist).HasMaxLength(255);
            entity.Property(e => e.OriginalKey).HasMaxLength(15);
            entity.Property(e => e.Tempo).HasMaxLength(15);
            entity.Property(e => e.TimeSignature).HasMaxLength(15);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.TransposedKey).HasMaxLength(15);

            entity.HasOne(d => d.Playlist).WithMany(p => p.Dbsongs)
                .HasForeignKey(d => d.PlaylistId)
                .HasConstraintName("FK__DBSongs__Playlis__18EBB532");
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Playlist__3213E83F93567F1A");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.LastDateViewed).HasColumnType("datetime");
            entity.Property(e => e.ListTitle).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.Playlists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Playlists__UserI__160F4887");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F7264FFE3");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.GoogleId)
                .HasMaxLength(255)
                .HasColumnName("GoogleID");
            entity.Property(e => e.LastName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
