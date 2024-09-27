using Microsoft.EntityFrameworkCore;
using Shared;

namespace Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "laptop",
                    Url = "laptop"
                },
                new Category
                {
                    Id = 2,
                    Name = "mobile",
                    Url = "mobile"
                },
                new Category
                {
                    Id = 3,
                    Name = "desktop",
                    Url = "desktop"
                }

           );


            modelBuilder.Entity<Product>().HasData(
                    new Product
                    {
                        Id = 1,
                        Title = "Product 1",
                        Description = "Description 1",
                        ImageUrl = "https://img.lovepik.com/element/40177/3459.png_1200.png",
                        Price = 1.99m,
                        CategoryId = 1,
                    },
                    new Product
                    {
                        Id = 2,
                        Title = "Product 2",
                        Description = "Description 2",
                        ImageUrl = "https://img.lovepik.com/element/40177/3459.png_1200.png",
                        Price = 2.99m,
                        CategoryId = 1,
                    },
                    new Product
                    {
                        Id = 3,
                        Title = "Product 3",
                        Description = "Description 3",
                        ImageUrl = "https://img.lovepik.com/element/40177/3459.png_1200.png",
                        Price = 3.99m,
                        CategoryId = 1,
                    }
                );
        }
    }
}
