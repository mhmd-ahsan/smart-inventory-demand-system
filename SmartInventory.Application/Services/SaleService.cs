using AutoMapper;
using SmartInventory.Application.Common.Responses;
using SmartInventory.Application.DTOs.SaleDtos;
using SmartInventory.Application.Interfaces.Product_Interfaces;
using SmartInventory.Application.Interfaces.Repo_Interfaces.Sale_Interface;
using SmartInventory.Application.Interfaces.Service_Interfaces.Sales_Interface;
using SmartInventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _repo;
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;

        public SaleService(ISaleRepository repo, IMapper mapper, IProductRepository productRepo)
        {
            _mapper = mapper;   
            _productRepo = productRepo;
            _repo = repo;
        }

        // Get All Sales
        public async Task<ServiceResponse<IEnumerable<SaleReadDto>>> GetAllSales()
        {
            try
            {
                var sales = await _repo.GetAllSalesAsync();

                var saleDtos = _mapper.Map<IEnumerable<SaleReadDto>>(sales);

                return ServiceResponse<IEnumerable<SaleReadDto>>.SuccessResponse(saleDtos, "Sales fetched successfully");
            }

            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<SaleReadDto>>.FailureResponse($"Error fetching sales: {ex.Message}");
            }
        }

        // Get Sales By Id
        public async Task<ServiceResponse<SaleReadDto>> GetSaleById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return ServiceResponse<SaleReadDto>.FailureResponse("Invalid sale id");
                }

                var sale = await _repo.GetSaleByIdAsync(id);

                if (sale == null)
                {
                    return ServiceResponse<SaleReadDto>.FailureResponse("Sale Not found");
                }

                var saleDto = _mapper.Map<SaleReadDto>(sale);
                return ServiceResponse<SaleReadDto>.SuccessResponse(saleDto ,"Sale fetched successfully");
            }
            catch (Exception ex)
            {
                return ServiceResponse<SaleReadDto>.FailureResponse($"Error fetching sales: {ex.Message}");

            }
        }

        // Create Sale
        public async Task<ServiceResponse<int>> CreateSale(CreateSaleDto dto)
        {
            try
            {
                var product = await _productRepo.GetByIdAsync(dto.ProductId);

                if(product == null)
                {
                    return ServiceResponse<int>.FailureResponse("Product Not found");
                }

                if(product.StockQuantity < dto.Quantity)
                {
                    return ServiceResponse<int>.FailureResponse("Insufficient Stock");
                }

                product.StockQuantity -= dto.Quantity;

                var sale = _mapper.Map<Sale>(dto);

                sale.TotalPrice = product.Price * dto.Quantity;

                sale.SaleDate = DateTime.UtcNow;

                await _repo.AddSaleAsync(sale);

                bool saved = await _repo.SaveChangesAsync();

                if (!saved)
                {
                    return ServiceResponse<int>.FailureResponse("Failed to create sale");

                }

                return ServiceResponse<int>.SuccessResponse(sale.Id, "Sale created successfully");
            }

            catch (Exception ex)
            {
                return ServiceResponse<int>.FailureResponse($"Error creating sale: {ex.Message}");
            }
        }

        // Update Sale
        public async Task<ServiceResponse<bool>> UpdateSale(int id, UpdateSaleDto dto)
        {
            try
            {
                var sale = await _repo.GetSaleByIdAsync(id);

                if(sale == null)
                {
                    return ServiceResponse<bool>.FailureResponse("Sale not found.");
                }

                _mapper.Map(sale, dto);

                _repo.UpdateSale(sale);

                bool saved = await _repo.SaveChangesAsync();

                if(!saved)
                {
                    return ServiceResponse<bool>.FailureResponse("Failed to update sale");
                }

                return ServiceResponse<bool>.SuccessResponse(true, "Sale updated successfully");
            }

            catch (Exception ex)
            {
                return ServiceResponse<bool>.FailureResponse($"Error updating sale: {ex.Message}");
            }
        }

        // Delete Sale
        public async Task<ServiceResponse<bool>> DeleteSale(int id)
        {
            try
            {
                var sale = await _repo.GetSaleByIdAsync(id);

                if(sale == null)
                {
                    return ServiceResponse<bool>.FailureResponse("Sale not found");
                }

                _repo.DeleteSale(sale);
                bool saved = await _repo.SaveChangesAsync();

                if (!saved)
                {
                    return ServiceResponse<bool>.FailureResponse("Failed to delete sale");
                }

                return ServiceResponse<bool>.SuccessResponse( true,"Sale deleted successfully");
            }

            catch(Exception ex)
            {
                return ServiceResponse<bool>.FailureResponse($"Error updating sale: {ex.Message}");
            }
        }
    }
}
