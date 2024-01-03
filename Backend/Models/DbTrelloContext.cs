using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrelloMVC.Models;

public partial class DbTrelloContext : DbContext
{
    public DbTrelloContext()
    {
    }

    public DbTrelloContext(DbContextOptions<DbTrelloContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<List> Lists { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=db_Trello.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>(entity =>
        {
            entity.Property(e => e.CreationDate)
                .HasColumnType("DATE")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.IdList)
                .HasColumnType("INT")
                .HasColumnName("Id_List");

            entity.HasOne(d => d.IdListNavigation).WithMany(p => p.Cards)
                .HasForeignKey(d => d.IdList)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(e => e.CreationDate)
                .HasColumnType("DATE")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.IdCard)
                .HasColumnType("INT")
                .HasColumnName("Id_Card");

            entity.HasOne(d => d.IdCardNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdCard)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<List>(entity =>
        {
            entity.HasKey(e => e.IdList);

            entity.Property(e => e.IdList).HasColumnName("Id_List");
            entity.Property(e => e.IdProject).HasColumnName("Id_Project");
            entity.Property(e => e.NameList).HasColumnName("Name_List");

            entity.HasOne(d => d.IdProjectNavigation).WithMany(p => p.Lists).HasForeignKey(d => d.IdProject);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.Property(e => e.CreationDate)
                .HasColumnType("DATE")
                .HasColumnName("Creation_Date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
