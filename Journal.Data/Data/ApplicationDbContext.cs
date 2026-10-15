// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using Journal.Models;

namespace Journal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<JournalItem> JournalItems { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JournalItem>()
                .Property(i => i.Price).HasPrecision(18, 2);

            modelBuilder.Entity<ProductType>().HasData(
                new ProductType { Id = 1, Name = "Leather Wrapped", Subtitle = "A5/A6 Size", Section = CatalogSection.Journal, SortOrder = 1 },
                new ProductType { Id = 2, Name = "Feather Wrapped", Subtitle = "A5/A6 Size", Section = CatalogSection.Journal, SortOrder = 2 },
                new ProductType { Id = 3, Name = "Crocodile Skin", Subtitle = "A4/A5", Section = CatalogSection.Journal, SortOrder = 3 },
                new ProductType { Id = 4, Name = "Custom Wrapped", Subtitle = "Any Size", Section = CatalogSection.Journal, SortOrder = 4 },
                new ProductType { Id = 5, Name = "Charms & Beads", Subtitle = "", Section = CatalogSection.Accessory, SortOrder = 1 },
                new ProductType { Id = 6, Name = "Ruled Notebook", Subtitle = "", Section = CatalogSection.Accessory, SortOrder = 2 },
                new ProductType { Id = 7, Name = "Pen & Pen Holder", Subtitle = "", Section = CatalogSection.Accessory, SortOrder = 3 },
                new ProductType { Id = 8, Name = "PVC Insert", Subtitle = "", Section = CatalogSection.Accessory, SortOrder = 4 }
            );

            modelBuilder.Entity<JournalItem>().HasData(
                new JournalItem { Id = 1, Code = "leather01", Title = "Classic Leather Journal", Description = "Hand-wrapped full-grain leather.", Price = 1500, IsFeatured = true, SortOrder = 1, ProductTypeId = 1 },
                new JournalItem { Id = 2, Code = "leather02", Title = "Brown Leather Journal", Description = "Refillable A6 cover.", Price = 1300, SortOrder = 2, ProductTypeId = 1 }
            );
        }
    }
}