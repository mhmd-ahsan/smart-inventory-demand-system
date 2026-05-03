using AutoMapper;
using SmartInventory.Application.Common.Responses;
using SmartInventory.Application.DTOs.SupplierDtos;
using SmartInventory.Application.Interfaces.Repo_Interfaces;
using SmartInventory.Application.Interfaces.Service_Interfaces.Supplier_Interface;
using SmartInventory.Domain.Entities;

namespace SmartInventory.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repo;
        private readonly IMapper _mapper;

        public SupplierService(
            ISupplierRepository repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // Get All Suppliers
        public async Task<ServiceResponse<IEnumerable<SupplierReadDto>>> GetAllSupplier()
        {
            try
            {
                var suppliers =
                    await _repo.GetAllSuppliersAsync();

                var supplierDtos =
                    _mapper.Map<
                        IEnumerable<SupplierReadDto>>
                        (suppliers);

                return ServiceResponse
                    <IEnumerable<SupplierReadDto>>
                    .SuccessResponse(
                        supplierDtos,
                        "Suppliers fetched successfully"
                    );
            }
            catch (Exception ex)
            {
                return ServiceResponse
                    <IEnumerable<SupplierReadDto>>
                    .FailureResponse(
                        $"An error occurred while fetching suppliers: {ex.Message}"
                    );
            }
        }

        // Get Supplier By Id
        public async Task<ServiceResponse<SupplierReadDto>> GetSupplierById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return ServiceResponse
                        <SupplierReadDto>
                        .FailureResponse(
                            "Invalid supplier id"
                        );
                }

                var supplier =
                    await _repo.GetSupplierByIdAsyn(id);

                if (supplier == null)
                {
                    return ServiceResponse
                        <SupplierReadDto>
                        .FailureResponse(
                            "Supplier not found"
                        );
                }

                var supplierDto =
                    _mapper.Map<SupplierReadDto>
                    (supplier);

                return ServiceResponse
                    <SupplierReadDto>
                    .SuccessResponse(
                        supplierDto,
                        "Supplier fetched successfully"
                    );
            }
            catch (Exception ex)
            {
                return ServiceResponse
                    <SupplierReadDto>
                    .FailureResponse(
                        $"An error occurred while fetching supplier: {ex.Message}"
                    );
            }
        }

        // Add Supplier
        public async Task<ServiceResponse<int>> AddSupplier(CreateSupplierDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return ServiceResponse<int>
                        .FailureResponse(
                            "Supplier data is required"
                        );
                }

                if (string.IsNullOrWhiteSpace(dto.Name))
                {
                    return ServiceResponse<int>
                        .FailureResponse(
                            "Supplier name is required"
                        );
                }

                var supplier =
                    _mapper.Map<Supplier>(dto);

                await _repo.AddSupplierAsync(supplier);

                bool saved =
                    await _repo.SaveChangesAsync();

                if (!saved)
                {
                    return ServiceResponse<int>
                        .FailureResponse(
                            "Failed to save supplier"
                        );
                }

                return ServiceResponse<int>
                    .SuccessResponse(
                        supplier.Id,
                        "Supplier created successfully"
                    );
            }
            catch (Exception ex)
            {
                return ServiceResponse<int>
                    .FailureResponse(
                        $"An error occurred while creating supplier: {ex.Message}"
                    );
            }
        }

        // Update Supplier
        public async Task<ServiceResponse<bool>> UpdateSupplier( int id, UpdateSupplierDto dto)
        {
            try
            {
                if (id <= 0)
                {
                    return ServiceResponse<bool>
                        .FailureResponse(
                            "Invalid supplier id"
                        );
                }

                if (dto == null)
                {
                    return ServiceResponse<bool>
                        .FailureResponse(
                            "Supplier data is required"
                        );
                }

                var supplier =
                    await _repo.GetSupplierByIdAsyn(id);

                if (supplier == null)
                {
                    return ServiceResponse<bool>
                        .FailureResponse(
                            "Supplier not found"
                        );
                }

                _mapper.Map(dto, supplier);

                _repo.UpdateSupplier(supplier);

                bool saved =
                    await _repo.SaveChangesAsync();

                if (!saved)
                {
                    return ServiceResponse<bool>
                        .FailureResponse(
                            "Failed to update supplier"
                        );
                }

                return ServiceResponse<bool>
                    .SuccessResponse(
                        true,
                        "Supplier updated successfully"
                    );
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>
                    .FailureResponse(
                        $"An error occurred while updating supplier: {ex.Message}"
                    );
            }
        }

        // Delete Supplier
        public async Task<ServiceResponse<bool>> DeleteSupplier(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return ServiceResponse<bool>
                        .FailureResponse(
                            "Invalid supplier id"
                        );
                }

                var supplier =
                    await _repo.GetSupplierByIdAsyn(id);

                if (supplier == null)
                {
                    return ServiceResponse<bool>
                        .FailureResponse(
                            "Supplier not found"
                        );
                }

                _repo.RemoveSupplier(supplier);

                bool saved =
                    await _repo.SaveChangesAsync();

                if (!saved)
                {
                    return ServiceResponse<bool>
                        .FailureResponse(
                            "Failed to delete supplier"
                        );
                }

                return ServiceResponse<bool>
                    .SuccessResponse(
                        true,
                        "Supplier deleted successfully"
                    );
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>
                    .FailureResponse(
                        $"An error occurred while deleting supplier: {ex.Message}"
                    );
            }
        }
    }
}