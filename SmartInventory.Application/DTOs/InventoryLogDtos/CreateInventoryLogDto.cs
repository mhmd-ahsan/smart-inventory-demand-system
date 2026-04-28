using SmartInventory.Domain.Entities;
using SmartInventory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.DTOs.InventoryLogDtos
{
    public class CreateInventoryLogDto
    {
        public int ProductId { get; set; }
        public int ChangeQuantity { get; set; } // + or -
        public InventoryType Type { get; set; } // IN / OUT
        public DateTime Date { get; set; }
    }
}
