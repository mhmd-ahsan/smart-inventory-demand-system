using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.DTOs.DashboardDtos
{
    public class DemandAnalysisDto
    {
        public string ProductName { get; set; }
        public int TotalSold { get; set; }
        public double AvgSalesPerDay { get; set; }
        public int CurrentStock { get; set; }
        public bool NeedsRestock { get; set; }
    }
}
