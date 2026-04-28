using SmartInventory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.DTOs.InventoryLogDtos
{
    public class InventoryLogReadDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int ChangeQuantity { get; set; }

        public InventoryType Type { get; set; }

        public DateTime Date { get; set; }
    }
}
