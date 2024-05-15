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

    public virtual DbSet<tblComment> tblComments { get; set; }

    public virtual DbSet<tblUser> tblUsers { get; set; }

    public virtual DbSet<tblVideo> tblVideos { get; set; }

    public virtual DbSet<tblVideosLiked> tblVideosLikes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=VRhfo.DB;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<tblComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblComme__3214EC0771597C95");

            entity.ToTable("tblComment");

            entity.Property(e => e.Content)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DatePosted).HasColumnType("datetime");
        });

        modelBuilder.Entity<tblUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblUser__3214EC07D2A703A1");

            entity.ToTable("tblUser");

            entity.Property(e => e.Auth0UserId)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationDate).HasColumnType("date");
            entity.Property(e => e.SubscribedDate).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(120);
        });

        modelBuilder.Entity<tblVideo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tblVideo__3214EC0794BE693B");

            entity.ToTable("tblVideo");

            entity.Property(e => e.Category).HasMaxLength(255);
            entity.Property(e => e.ContentWarning).HasMaxLength(255);
            entity.Property(e => e.Genre).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UploadDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<tblVideosLiked>(entity =>
        {
            entity.HasKey(e => new { e.UserID, e.VideoID }).HasName("PK__tblVideo__AC269D88C5FCF69C");

            entity.ToTable("tblVideosLiked");

            entity.Property(e => e.LikedDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.tblVideosLikeds)
                .HasForeignKey(d => d.UserID)
                .HasConstraintName("FK__tblVideos__UserI__2A4B4B5E");

            entity.HasOne(d => d.Video).WithMany(p => p.tblVideosLikeds)
                .HasForeignKey(d => d.VideoID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblVideos__Video__2B3F6F97");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
