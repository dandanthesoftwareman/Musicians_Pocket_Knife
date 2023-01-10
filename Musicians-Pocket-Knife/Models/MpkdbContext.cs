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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=mpkserver.database.windows.net;Initial Catalog=MPKDB; User Id=mpkdbadmin; Password=#mpklogin633;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dbsong>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DBSongs__3213E83FEDF60688");

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
                .HasConstraintName("FK__DBSongs__Playlis__7F2BE32F");
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Playlist__3213E83FD9B550EA");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ListTitle).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.Playlists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Playlists__UserI__75A278F5");
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
