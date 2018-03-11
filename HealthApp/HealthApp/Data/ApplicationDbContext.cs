using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HealthApp.Models;

namespace HealthApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<MedicalRecord> MedicalRecords { get; set; }

        public DbSet<Meal> Meals { get; set; }

        public DbSet<Routine> Routines { get; set; }

        public DbSet<UserMeal> UserMeals { get; set; }

        public DbSet<Measurement> Measurements { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<UserMeal>()
              .HasIndex(m => m.UserMealId);
        }
    }
}
