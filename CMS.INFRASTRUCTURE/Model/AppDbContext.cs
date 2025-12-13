using System;
using CMS.CORE.Data;
using Microsoft.EntityFrameworkCore;

namespace CMS.INFRASTRUCTURE.Model;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public DbSet<User> Users;
    public DbSet<UserRole> Roles;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User table
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(x => x.Email);
        });

        // Role table
        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasIndex(u => u.Id);
        });
    }


}
