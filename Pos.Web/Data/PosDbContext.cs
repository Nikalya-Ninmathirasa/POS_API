using Microsoft.EntityFrameworkCore;
using Pos.Web.Models;

namespace Pos.Web.Data
{
    public class PosDbContext :DbContext  //inherit features from DbContext
    {
        public PosDbContext(DbContextOptions<PosDbContext>options) :base(options)
        {

        }

        // create table with name of Categories
        public DbSet<Category> Categories { get; set; }

        // create table with name of Products
        public DbSet<Product> Products { get; set; }


        // model builder for 1 to many relationship
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);
        }

    }

    
}
