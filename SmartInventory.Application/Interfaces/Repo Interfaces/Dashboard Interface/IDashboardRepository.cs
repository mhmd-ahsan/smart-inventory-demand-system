using SmartInventory.Application.DTOs.DashboardDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.Interfaces.Repo_Interfaces.Dashboard_Interface
{
    public interface IDashboardRepository
    {
        Task<DashboardReadDto> GetDashboardDataAsync();
        Task<List<DemandAnalysisDto>> GetDemandAnalysisAsync();
    }
}
