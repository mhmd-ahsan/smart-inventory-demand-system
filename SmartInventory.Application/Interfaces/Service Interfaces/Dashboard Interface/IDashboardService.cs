using SmartInventory.Application.Common.Responses;
using SmartInventory.Application.DTOs.DashboardDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.Interfaces.Service_Interfaces.Dashboard_Interface
{
    public interface IDashboardService
    {
        Task<ServiceResponse<DashboardReadDto>> GetDashboardDataAsync();
    }
}
