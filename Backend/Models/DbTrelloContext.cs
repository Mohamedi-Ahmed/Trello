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
            entity.HasOne(d => d.IdListNavigation).WithMany(p => p.Cards).HasForeignKey(d => d.IdList);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasOne(d => d.IdCardNavigation).WithMany(p => p.Comments).HasForeignKey(d => d.IdCard);
        });

        modelBuilder.Entity<List>(entity =>
        {
            entity.HasOne(d => d.IdProjectNavigation).WithMany(p => p.Lists).HasForeignKey(d => d.IdProject);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
