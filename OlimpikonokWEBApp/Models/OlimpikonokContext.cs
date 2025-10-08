using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OlimpikonokWEBApp.Models;

public partial class OlimpikonokContext : DbContext
{
    public OlimpikonokContext()
    {
    }

    public OlimpikonokContext(DbContextOptions<OlimpikonokContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Orszag> Orszags { get; set; }

    public virtual DbSet<Sportag> Sportags { get; set; }

    public virtual DbSet<Sportolo> Sportolos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("SERVER=localhost;PORT=3306;DATABASE=olimpikonok;USER=root;PASSWORD=;SSL MODE=none;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Orszag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("orszag");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Fovaros).HasMaxLength(32);
            entity.Property(e => e.Nepesseg).HasColumnType("int(11)");
            entity.Property(e => e.Nev).HasMaxLength(32);
        });

        modelBuilder.Entity<Sportag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sportag");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Megnevezes)
                .HasMaxLength(32)
                .HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<Sportolo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sportolo");

            entity.HasIndex(e => e.OrszagId, "OrszagId");

            entity.HasIndex(e => e.SportagId, "SporagId");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Ermek)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.IndexKep)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("blob");
            entity.Property(e => e.Kep)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("mediumblob");
            entity.Property(e => e.Neme)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("NEme");
            entity.Property(e => e.Nev)
                .HasMaxLength(32)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.OrszagId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.SportagId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.SzulDatum)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date");

            entity.HasOne(d => d.Orszag).WithMany(p => p.Sportolos)
                .HasForeignKey(d => d.OrszagId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("sportolo_ibfk_2");

            entity.HasOne(d => d.Sportag).WithMany(p => p.Sportolos)
                .HasForeignKey(d => d.SportagId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("sportolo_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
