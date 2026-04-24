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
    public class InventoryLogConfiguration : IEntityTypeConfiguration<InventoryLog>
    {
        public void Configure(EntityTypeBuilder<InventoryLog> builder)
        {
            builder.ToTable("InventoryLogs");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.ChangeQuantity)
               .IsRequired();

            builder.Property(i => i.Type)
                .IsRequired();

            builder.Property(i => i.Date)
                .IsRequired();

            builder.HasOne(i => i.Product)
                .WithMany(p => p.InventoryLogs)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
