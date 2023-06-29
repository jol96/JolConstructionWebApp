using JolConstruction.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JolConstruction.Data
{
    public class ApplicationDbContext: IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categorys { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Cover Photo", DisplayOrder = 1},
                new Category { Id = 2, Name = "Home Page Project", DisplayOrder = 2 });

            modelBuilder.Entity<Post>().HasData(
                new Post { 
                    Id = 1, 
                    Title = "Cover Photo", 
                    Description = "Cover Photo",
                    CategoryId = 1
                },
                new Post
                {
                    Id = 2,
                    Title = "New Build",
                    Description = "A new build project completed in 2022",
                    CategoryId = 2
                });
        }
    }
}
