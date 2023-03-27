using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IdeaWeb.Models;

namespace IdeaWeb.Data;

public class IdeaWebContext : DbContext
{
    public IdeaWebContext(DbContextOptions<IdeaWebContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.comments)
            .WithOne(c => c.user)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .HasMany(u => u.ratings)
            .WithOne(r => r.user)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Ideas)
            .WithOne(i => i.User)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.userRoles)
            .WithOne(ur => ur.user)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.ratings)
            .WithOne(r => r.user)
            .HasForeignKey(r => r.userId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<User>()
            .HasMany(u => u.View)
            .WithOne(v => v.user)
            .OnDelete(DeleteBehavior.Restrict);
        // Configure other relationships

        base.OnModelCreating(modelBuilder);
    }



    public DbSet<IdeaWeb.Models.Category> Category { get; set; }
    public DbSet<IdeaWeb.Models.CloseDateAcedamic> CloseDateAcedamic { get; set; }
    public DbSet<IdeaWeb.Models.Comment> Comment { get; set; }
    public DbSet<IdeaWeb.Models.Department> Department { get; set; }
    public DbSet<IdeaWeb.Models.View> View { get; set; }
    public DbSet<IdeaWeb.Models.Idea> Idea { get; set; }
    public DbSet<IdeaWeb.Models.Rating> Rating { get; set; }
    public DbSet<IdeaWeb.Models.Role> Role { get; set; }
    public DbSet<IdeaWeb.Models.User> User { get; set; }
    public DbSet<IdeaWeb.Models.UserRole> UserRole { get; set; }


}
