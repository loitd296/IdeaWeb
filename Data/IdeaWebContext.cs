using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IdeaWeb.Models;

namespace IdeaWeb.Data;

    public class IdeaWebContext : DbContext
    {
        public IdeaWebContext (DbContextOptions<IdeaWebContext> options)
            : base(options)
        {
        }

        public DbSet<IdeaWeb.Models.Category> Category { get; set; }
        public DbSet<IdeaWeb.Models.CloseDateAcedamic> CloseDateAcedamic { get; set; }

        public DbSet<IdeaWeb.Models.Comment> Comment { get; set; }

        public DbSet<IdeaWeb.Models.Department> Departments { get; set; }

        public DbSet<IdeaWeb.Models.Document> Documents { get; set; }

        public DbSet<IdeaWeb.Models.Ideas> Ideas { get; set; }


        public DbSet<IdeaWeb.Models.Rating> Rating { get; set; }

        public DbSet<IdeaWeb.Models.Roles> Roles { get; set; }

        public DbSet<IdeaWeb.Models.User> User { get; set; }
        public DbSet<IdeaWeb.Models.UserRole> UserRoles { get; set; }


    }
