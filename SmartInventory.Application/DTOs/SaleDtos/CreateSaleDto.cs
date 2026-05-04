using SmartInventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.DTOs.SaleDtos
{
    public class CreateSaleDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
