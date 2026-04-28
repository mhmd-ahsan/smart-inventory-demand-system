using AutoMapper;
using SmartInventory.Application.DTOs.InventoryLogDtos;
using SmartInventory.Application.Interfaces.Product_Interfaces;
using SmartInventory.Application.Interfaces.Repo_Interfaces.Inventory_Interface;
using SmartInventory.Application.Interfaces.Service_Interfaces.Inventory_Interface;
using SmartInventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.Services
{
    public class InventoryLogService : IInventoryLogService
    {
        private readonly IInventoryLogRepository _repo;
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;

        public InventoryLogService(IInventoryLogRepository repo, IMapper mapper, IProductRepository productRepo)
        {
            _mapper = mapper;
            _productRepo = productRepo;
            _repo = repo;
        }

        // Get All Logs
        public async Task<IEnumerable<InventoryLogReadDto>> GetAllAsync()
        {
            var logs = await _repo.GetAllInventoryLogAsync();

            return _mapper.Map<IEnumerable<InventoryLogReadDto>>(logs);
        }

        // Get by Id
        public async Task<InventoryLogReadDto> GetByIdAsync(int id)
        {
            var log = await _repo.GetByIdAsync(id);

            if (log is null)
                throw new Exception(
                "Not Found");

            return _mapper.Map<InventoryLogReadDto>(log);
        }

        // Add InventoryLog
        public async Task CreateAsync(CreateInventoryLogDto dto)
        {
            var product = await _productRepo.GetByIdAsync(dto.ProductId);

            if (product is null)
                throw new Exception(
                "Product not found");
            
            if(dto.Type == Domain.Enums.InventoryType.IN)
            {
                product.StockQuantity += dto.ChangeQuantity;
            }

            if(dto.Type == Domain.Enums.InventoryType.OUT)
            {
                if (product.StockQuantity <
                   dto.ChangeQuantity)
                    throw new Exception(
                     "Insufficient stock");

                product.StockQuantity -= dto.ChangeQuantity;
            }

            var log = _mapper.Map<InventoryLog>(dto);

            log.Date = DateTime.UtcNow;

            await _repo.AddInventoryAsync(log);
            await _repo.SaveChangesAsync();
        }

        // Update Inventory Log
        public async Task UpdateAsync(int id, UpdateInventoryLogDto dto)
        {
            var log = await _repo.GetByIdAsync(id);

            if (log is null)
                throw new Exception(
                "Not Found");

            _mapper.Map(dto, log);
             _repo.UpdateInventory(log);

            await _repo.SaveChangesAsync();
        }

        // Delete Log 
        public async Task DeleteAsync(int id)
        {
            var log = await _repo.GetByIdAsync(id);

            if (log is null)
                throw new Exception(
                    "Not Found");

             _repo.DeleteInventory(log);
            await _repo.SaveChangesAsync();
        }
    }
}
