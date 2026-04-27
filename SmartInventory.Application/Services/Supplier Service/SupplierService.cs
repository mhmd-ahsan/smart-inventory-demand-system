using AutoMapper;
using SmartInventory.Application.DTOs.SupplierDtos;
using SmartInventory.Application.Interfaces.Repo_Interfaces;
using SmartInventory.Application.Interfaces.Service_Interfaces.Supplier_Interface;
using SmartInventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repo;
        private readonly IMapper _mapper;

        public SupplierService(ISupplierRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // Get All Suppliers
        public async Task<IEnumerable<SupplierReadDto>> GetAllSupplier()
        {
            var suppliers = await _repo.GetAllSuppliersAsync();

            return _mapper.Map<
            IEnumerable<SupplierReadDto>>
            (suppliers);
        }

        // Get Supplier By Id
        public async Task<SupplierReadDto?> GetSupplierById(int id)
        {
            var supplier = await _repo.GetSupplierByIdAsyn(id);

            if(supplier == null)
                return null;
            return _mapper.Map<SupplierReadDto>(supplier);
        }

        // Add Supplier
        public async Task AddSupplier(CreateSupplierDto dto)
        {
            var supplier = _mapper.Map<Supplier>(dto);

            await _repo.AddSupplierAsync(supplier);

            await _repo.SaveChangesAsync();
        }

        // Update Supplier
        public async Task UpdateSupplier(int id, UpdateSupplierDto dto)
        {
            var supplier = await _repo.GetSupplierByIdAsyn(id);

            if (supplier == null)
                throw new Exception(
                 "Supplier not found");
            _mapper.Map(dto, supplier);

             _repo.UpdateSupplier(supplier);

            await _repo.SaveChangesAsync();
        }

        // Delete SUpplier
        public async Task DeleteSupplier(int id)
        {
            var supplier = await _repo.GetSupplierByIdAsyn(id);

            if (supplier == null)
                throw new Exception(
                 "Supplier not found");

            _repo.RemoveSupplier(supplier);
            await _repo.SaveChangesAsync();
        }
    }
}
