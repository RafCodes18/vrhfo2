using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VRhfo.PL;

public partial class VRhfoEntities : DbContext
{
    public VRhfoEntities()
    {
    }

    public VRhfoEntities(DbContextOptions<VRhfoEntities> options)
        : base(options)
    {
    }

    public virtual DbSet<tblUser> tblUsers { get; set; }

    public virtual DbSet<tblVideo> tblVideos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=VRhfo.DB;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<tblUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblUser__3214EC079F64D3FC");

            entity.ToTable("tblUser");

            entity.Property(e => e.Auth0UserId)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.RegistrationDate).HasColumnType("date");
            entity.Property(e => e.SubscribedDate).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(120);
        });

        modelBuilder.Entity<tblVideo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblVideo__3214EC079FD0C44A");

            entity.ToTable("tblVideo");

            entity.Property(e => e.ContentWarning).HasMaxLength(255);
            entity.Property(e => e.Genre).HasMaxLength(50);
            entity.Property(e => e.Category).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UploadDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
