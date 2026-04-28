using SmartInventory.Domain.Entities;
using SmartInventory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.DTOs.InventoryLogDtos
{
    public class UpdateInventoryLogDto
    {
        public int ChangeQuantity { get; set; }

        public InventoryType Type { get; set; }
    }
}
