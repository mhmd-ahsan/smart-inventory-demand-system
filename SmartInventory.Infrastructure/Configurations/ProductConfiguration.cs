using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartInventory.Domain.Entities;

namespace SmartInventory.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.StockQuantity)
                .IsRequired();

            // Category Relation
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Supplier relation
            builder.HasOne(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            // Sales relation
            builder.HasMany(p => p.Sales)
                .WithOne(s => s.Product)
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Inventory logs relation
            builder.HasMany(p => p.InventoryLogs)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}