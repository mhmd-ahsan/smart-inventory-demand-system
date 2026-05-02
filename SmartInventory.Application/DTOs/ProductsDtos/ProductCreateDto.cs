using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.DTOs.ProductsDtos
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
    }
}
