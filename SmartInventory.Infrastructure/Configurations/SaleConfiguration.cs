using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartInventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Infrastructure.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            // Table Name
            builder.ToTable("Sales");

            // Primary Key
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Quantity)
                .IsRequired();

            builder.Property(s => s.TotalPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(s => s.SaleDate)
                .IsRequired();

            builder.HasOne(s => s.Product)
                .WithMany(p => p.Sales)
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Cascade);




        }
    }
}
