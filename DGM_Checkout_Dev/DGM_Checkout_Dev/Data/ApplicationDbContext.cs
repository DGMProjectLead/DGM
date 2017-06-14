using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DGM_Checkout_dev.Models;

namespace DGM_Checkout_dev.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Rental> Rental { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Models.Type> Type { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Inventory>().ToTable("Inventory");
            builder.Entity<Location>().ToTable("Location");
            builder.Entity<Rental>().ToTable("Rental");
            builder.Entity<Status>().ToTable("Status");
            builder.Entity<Models.Type>().ToTable("Type");
            builder.Entity<User>().ToTable("User");
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
