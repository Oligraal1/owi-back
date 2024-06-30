using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using owi_back.Models;

namespace owi_back.Context;

public partial class OwidbContext : DbContext
{
    public OwidbContext()
    {
    }

    public OwidbContext(DbContextOptions<OwidbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Listing> Listings { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<owi_back.Models.Task> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:owiserver.database.windows.net,1433;Initial Catalog=owidb;Persist Security Info=False;User ID=oligraal;Password=Ecureuil22;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__comments__3213E83FCC322038");

            entity.ToTable("comments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.User)
                .HasMaxLength(255)
                .HasColumnName("user");

            entity.HasOne(d => d.Task).WithMany(p => p.Comments)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("comments_task_id_foreign");
        });

        modelBuilder.Entity<Listing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__listings__3213E83F9CC2368E");

            entity.ToTable("listings");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");

            entity.HasOne(d => d.Project).WithMany(p => p.Listings)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("listings_project_id_foreign");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__projects__3213E83F03E53C97");

            entity.ToTable("projects");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Deadline)
                .HasColumnType("datetime")
                .HasColumnName("deadline");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<owi_back.Models.Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tasks__3213E83F4F170CB5");

            entity.ToTable("tasks");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Deadline)
                .HasColumnType("datetime")
                .HasColumnName("deadline");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.ListingId).HasColumnName("listing_id");
            entity.Property(e => e.ModifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("modified_at");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Tag)
                .HasMaxLength(255)
                .HasColumnName("tag");

            entity.HasOne(d => d.Listing).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ListingId)
                .HasConstraintName("tasks_listing_id_foreign");
        });
        modelBuilder.Entity<owi_back.Models.Task>()
    .HasOne(t => t.Listing)
    .WithMany(l => l.Tasks)
    .HasForeignKey(t => t.ListingId)
    .OnDelete(DeleteBehavior.Cascade);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
