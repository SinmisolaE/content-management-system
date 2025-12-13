using System;
using CMS.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Model;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public DbSet<User> Users {get; set;}
    public DbSet<UserRole> Roles {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User table
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(x => x.Email).IsUnique();

            entity.HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId);
        });

        // Role table
        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(x => x.Id);
        });
    }


}
