using SmartInventory.Application.Common.Responses;
using SmartInventory.Application.DTOs.DashboardDtos;
using SmartInventory.Application.Interfaces.Repo_Interfaces.Dashboard_Interface;
using SmartInventory.Application.Interfaces.Service_Interfaces.Dashboard_Interface;

namespace SmartInventory.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _repo;

        public DashboardService(IDashboardRepository repo)
        {
            _repo = repo;
        }

        public async Task<ServiceResponse<DashboardReadDto>> GetDashboardDataAsync()
        {
            try
            {
                var data =
                    await _repo.GetDashboardDataAsync();

                return ServiceResponse<DashboardReadDto>
                    .SuccessResponse(
                        data,
                        "Dashboard data fetched successfully"
                    );
            }
            catch (Exception ex)
            {
                return ServiceResponse<DashboardReadDto>
                    .FailureResponse(
                        $"Error fetching dashboard data: {ex.Message}"
                    );
            }
        }
    }
}