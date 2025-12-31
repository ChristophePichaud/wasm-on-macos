using Microsoft.EntityFrameworkCore;
using PersonalSoftwareManager.Contracts.Models;

namespace PersonalSoftwareManager.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Software> Software { get; set; } = null!;
    public DbSet<OpenSourceProject> OpenSourceProjects { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Screenshot> Screenshots { get; set; } = null!;
    public DbSet<Url> Urls { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Software entity
        modelBuilder.Entity<Software>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Version).HasMaxLength(50);
            
            entity.HasMany(e => e.Comments)
                .WithOne(c => c.Software)
                .HasForeignKey(c => c.SoftwareId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasMany(e => e.Screenshots)
                .WithOne(s => s.Software)
                .HasForeignKey(s => s.SoftwareId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasMany(e => e.Urls)
                .WithOne(u => u.Software)
                .HasForeignKey(u => u.SoftwareId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure OpenSourceProject entity
        modelBuilder.Entity<OpenSourceProject>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.GitHubUrl).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Owner).HasMaxLength(100);
            entity.Property(e => e.Language).HasMaxLength(50);
            
            entity.HasMany(e => e.Comments)
                .WithOne(c => c.OpenSourceProject)
                .HasForeignKey(c => c.OpenSourceProjectId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasMany(e => e.Screenshots)
                .WithOne(s => s.OpenSourceProject)
                .HasForeignKey(s => s.OpenSourceProjectId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasMany(e => e.Urls)
                .WithOne(u => u.OpenSourceProject)
                .HasForeignKey(u => u.OpenSourceProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Comment entity
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Content).IsRequired().HasMaxLength(5000);
        });

        // Configure Screenshot entity
        modelBuilder.Entity<Screenshot>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FileName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.FilePath).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Description).HasMaxLength(500);
        });

        // Configure Url entity
        modelBuilder.Entity<Url>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Link).IsRequired().HasMaxLength(1000);
            entity.Property(e => e.Description).HasMaxLength(500);
        });
    }
}
