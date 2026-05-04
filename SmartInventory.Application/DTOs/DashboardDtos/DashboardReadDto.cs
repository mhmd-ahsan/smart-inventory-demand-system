using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.DTOs.DashboardDtos
{
    public class DashboardReadDto
    {
        public int TotalProducts { get; set; }
        public int TotalCategories { get; set; }
        public int TotalSuppliers { get; set; }
        public int TotalSales { get; set; }

        public decimal TotalRevenue { get; set; }

        public int LowStockProducts { get; set; }

        public List<string> TopSellingProducts { get; set; }
    }
}
