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

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserProject> UserProjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>(entity =>
        {
            entity.Property(e => e.DateCreation)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("DATETIME");
            entity.Property(e => e.Title).HasColumnType("NVARCHAR(255)");

            entity.HasOne(d => d.Creator).WithMany(p => p.Cards).HasForeignKey(d => d.CreatorId);

            entity.HasOne(d => d.IdListNavigation).WithMany(p => p.Cards)
                .HasForeignKey(d => d.IdList)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(e => e.DateCreation)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("DATETIME");

            entity.HasOne(d => d.IdCardNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdCard)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.User).WithMany(p => p.Comments).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<List>(entity =>
        {
            entity.Property(e => e.Name).HasColumnType("NVARCHAR(255)");

            entity.HasOne(d => d.IdProjectNavigation).WithMany(p => p.Lists)
                .HasForeignKey(d => d.IdProject)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.Property(e => e.DateCreation)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("DATETIME");
            entity.Property(e => e.Name).HasColumnType("NVARCHAR(255)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).HasColumnType("NVARCHAR(255)");
            entity.Property(e => e.FirstName).HasColumnType("NVARCHAR(255)");
            entity.Property(e => e.LastName).HasColumnType("NVARCHAR(255)");
            entity.Property(e => e.Password).HasColumnType("NVARCHAR(255)");
            entity.Property(e => e.UserName).HasColumnType("NVARCHAR(255)");
        });

        modelBuilder.Entity<UserProject>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ProjectId });

            entity.Property(e => e.UserRole)
                .HasDefaultValue("Membre")
                .HasColumnType("NVARCHAR(255)");

            entity.HasOne(d => d.Project).WithMany(p => p.UserProjects)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.UserProjects)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
