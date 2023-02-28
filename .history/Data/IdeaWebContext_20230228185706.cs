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

    public DbSet<IdeaWeb.Models.Category> Category { get; set; }
    public DbSet<IdeaWeb.Models.CloseDateAcedamic> CloseDateAcedamic { get; set; }
    public DbSet<IdeaWeb.Models.Comment> Comment { get; set; }
    public DbSet<IdeaWeb.Models.Department> Department { get; set; }
    public DbSet<IdeaWeb.Models.Document> Document { get; set; }
    public DbSet<IdeaWeb.Models.Idea> Idea { get; set; }
    public DbSet<IdeaWeb.Models.Rating> Rating { get; set; }
    public DbSet<IdeaWeb.Models.Role> Role { get; set; }
    public DbSet<IdeaWeb.Models.User> User { get; set; }
    public DbSet<IdeaWeb.Models.UserRole> UserRole { get; set; }


}
