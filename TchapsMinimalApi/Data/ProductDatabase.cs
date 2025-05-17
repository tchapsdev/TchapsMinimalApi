using Microsoft.EntityFrameworkCore;

namespace TchapsMinimalApi.Data
{
    public class ProductDatabase : DbContext
    {
        public ProductDatabase(DbContextOptions<ProductDatabase> options) : base(options)
        {

        }

        public DbSet<Model.Product> Products { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.Product>().ToTable("Products");
            modelBuilder.Entity<Model.Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Model.Product>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Model.Product>().Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Model.Product>().Property(p => p.Description).HasMaxLength(500);
            modelBuilder.Entity<Model.Product>().Property(p => p.Category).HasMaxLength(50);
            modelBuilder.Entity<Model.Product>().Property(p => p.ImageUrl).HasMaxLength(200);
            modelBuilder.Entity<Model.Product>().Property(p => p.IsAvailable).IsRequired();
        }

    }
}
