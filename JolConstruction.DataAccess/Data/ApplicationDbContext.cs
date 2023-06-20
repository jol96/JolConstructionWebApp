using JolConstruction.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace JolConstruction.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categorys { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Roofing", DisplayOrder = 1},
                new Category { Id = 2, Name = "Extensions", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Renovations", DisplayOrder = 3 });

            modelBuilder.Entity<Post>().HasData(
                new Post { Id = 1, Title = "Timber Framing", Description = "Here we have done some external framing" }
                ); ;
        }
    }
}
