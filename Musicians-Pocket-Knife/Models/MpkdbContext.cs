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

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer($"Data Source={Secret.server};Initial Catalog=MPKDB; User Id={Secret.username}; Password={Secret.password};");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
