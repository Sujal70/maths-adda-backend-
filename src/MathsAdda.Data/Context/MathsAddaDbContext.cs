using Microsoft.EntityFrameworkCore;
using MathsAdda.Data.Entities;

namespace MathsAdda.Data.Context;

public class MathsAddaDbContext : DbContext
{
    public MathsAddaDbContext(DbContextOptions<MathsAddaDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Subject> Subjects => Set<Subject>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.PasswordHash).IsRequired();
        });

        // Role configuration
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasData(
                new Role { Id = 1, Name = "Admin", Description = "System Administrator" },
                new Role { Id = 2, Name = "Student", Description = "Student User" },
                new Role { Id = 3, Name = "Teacher", Description = "Teacher/Instructor" },
                new Role { Id = 4, Name = "Parent", Description = "Parent/Guardian" }
            );
        });

        // RefreshToken configuration
        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasIndex(e => e.Token).IsUnique();
        });

        // Course configuration
        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
        });
    }
}