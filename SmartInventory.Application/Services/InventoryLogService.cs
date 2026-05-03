using AutoMapper;
using SmartInventory.Application.Common.Responses;
using SmartInventory.Application.DTOs.InventoryLogDtos;
using SmartInventory.Application.Interfaces.Product_Interfaces;
using SmartInventory.Application.Interfaces.Repo_Interfaces.Inventory_Interface;
using SmartInventory.Application.Interfaces.Service_Interfaces.Inventory_Interface;
using SmartInventory.Domain.Entities;
using SmartInventory.Domain.Enums;

namespace SmartInventory.Application.Services
{
    public class InventoryLogService: IInventoryLogService
    {
        private readonly IInventoryLogRepository _repo;
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;

        public InventoryLogService(
            IInventoryLogRepository repo,
            IMapper mapper,
            IProductRepository productRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _productRepo = productRepo;
        }

        // Get All Logs
        public async Task<ServiceResponse<IEnumerable<InventoryLogReadDto>>> GetAllAsync()
        {
            try
            {
                var logs =
                    await _repo.GetAllInventoryLogAsync();

                var logDtos =
                    _mapper.Map<
                        IEnumerable<InventoryLogReadDto>>
                        (logs);

                return ServiceResponse
                    <IEnumerable<InventoryLogReadDto>>
                    .SuccessResponse(
                        logDtos,
                        "Inventory logs fetched successfully"
                    );
            }
            catch (Exception ex)
            {
                return ServiceResponse
                    <IEnumerable<InventoryLogReadDto>>
                    .FailureResponse(
                        $"An error occurred while fetching inventory logs: {ex.Message}"
                    );
            }
        }

        // Get Log By Id
        public async Task<ServiceResponse<InventoryLogReadDto>> GetByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return ServiceResponse
                        <InventoryLogReadDto>
                        .FailureResponse(
                            "Invalid inventory log id"
                        );
                }

                var log =
                    await _repo.GetByIdAsync(id);

                if (log == null)
                {
                    return ServiceResponse
                        <InventoryLogReadDto>
                        .FailureResponse(
                            "Inventory log not found"
                        );
                }

                var logDto =
                    _mapper.Map<InventoryLogReadDto>
                    (log);

                return ServiceResponse
                    <InventoryLogReadDto>
                    .SuccessResponse(
                        logDto,
                        "Inventory log fetched successfully"
                    );
            }
            catch (Exception ex)
            {
                return ServiceResponse
                    <InventoryLogReadDto>
                    .FailureResponse(
                        $"An error occurred while fetching inventory log: {ex.Message}"
                    );
            }
        }

        // Add Inventory Log
        public async Task<ServiceResponse<int>> CreateAsync(CreateInventoryLogDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return ServiceResponse<int>
                        .FailureResponse(
                            "Inventory log data is required"
                        );
                }

                var product =
                    await _productRepo
                        .GetByIdAsync(dto.ProductId);

                if (product == null)
                {
                    return ServiceResponse<int>
                        .FailureResponse(
                            "Product not found"
                        );
                }

                // Stock Handling
                if (dto.Type == InventoryType.IN)
                {
                    product.StockQuantity +=
                        dto.ChangeQuantity;
                }

                if (dto.Type == InventoryType.OUT)
                {
                    if (product.StockQuantity <
                        dto.ChangeQuantity)
                    {
                        return ServiceResponse<int>
                            .FailureResponse(
                                "Insufficient stock"
                            );
                    }

                    product.StockQuantity -=
                        dto.ChangeQuantity;
                }

                var log =
                    _mapper.Map<InventoryLog>(dto);

                log.Date = DateTime.UtcNow;

                await _repo.AddInventoryAsync(log);

                bool saved =
                    await _repo.SaveChangesAsync();

                if (!saved)
                {
                    return ServiceResponse<int>
                        .FailureResponse(
                            "Failed to save inventory log"
                        );
                }

                return ServiceResponse<int>
                    .SuccessResponse(
                        log.Id,
                        "Inventory log created successfully"
                    );
            }
            catch (Exception ex)
            {
                return ServiceResponse<int>
                    .FailureResponse(
                        $"An error occurred while creating inventory log: {ex.Message}"
                    );
            }
        }

        // Update Inventory Log
        public async Task<ServiceResponse<bool>> UpdateAsync(int id, UpdateInventoryLogDto dto)
        {
            try
            {
                if (id <= 0)
                {
                    return ServiceResponse<bool>
                        .FailureResponse(
                            "Invalid inventory log id"
                        );
                }

                if (dto == null)
                {
                    return ServiceResponse<bool>
                        .FailureResponse(
                            "Inventory log data is required"
                        );
                }

                var log =
                    await _repo.GetByIdAsync(id);

                if (log == null)
                {
                    return ServiceResponse<bool>
                        .FailureResponse(
                            "Inventory log not found"
                        );
                }

                _mapper.Map(dto, log);

                _repo.UpdateInventory(log);

                bool saved =
                    await _repo.SaveChangesAsync();

                if (!saved)
                {
                    return ServiceResponse<bool>
                        .FailureResponse(
                            "Failed to update inventory log"
                        );
                }

                return ServiceResponse<bool>
                    .SuccessResponse(
                        true,
                        "Inventory log updated successfully"
                    );
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>
                    .FailureResponse(
                        $"An error occurred while updating inventory log: {ex.Message}"
                    );
            }
        }

        // Delete Inventory Log
        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return ServiceResponse<bool>
                        .FailureResponse(
                            "Invalid inventory log id"
                        );
                }

                var log =
                    await _repo.GetByIdAsync(id);

                if (log == null)
                {
                    return ServiceResponse<bool>
                        .FailureResponse(
                            "Inventory log not found"
                        );
                }

                _repo.DeleteInventory(log);

                bool saved =
                    await _repo.SaveChangesAsync();

                if (!saved)
                {
                    return ServiceResponse<bool>
                        .FailureResponse(
                            "Failed to delete inventory log"
                        );
                }

                return ServiceResponse<bool>
                    .SuccessResponse(
                        true,
                        "Inventory log deleted successfully"
                    );
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>
                    .FailureResponse(
                        $"An error occurred while deleting inventory log: {ex.Message}"
                    );
            }
        }
    }
}