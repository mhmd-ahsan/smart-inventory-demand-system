using SmartInventory.Domain.Common;
using SmartInventory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Domain.Entities
{
    public class InventoryLog : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int ChangeQuantity { get; set; } // + or -
        public InventoryType Type { get; set; } // IN / OUT

        public DateTime Date { get; set; }
    }
}
