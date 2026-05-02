using AutoMapper;
using SmartInventory.Application.DTOs.CategoryDtos;
using SmartInventory.Application.DTOs.InventoryLogDtos;
using SmartInventory.Application.DTOs.ProductsDtos;
using SmartInventory.Application.DTOs.SupplierDtos;
using SmartInventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Product Mapping
            CreateMap<Product, ProductReadDto>()
                .ForMember(des => des.SupplierName,
                opt => opt.MapFrom(s => s.Supplier.Name));

            CreateMap<ProductCreateDto, Product>();

            CreateMap<ProductUpdateDto, Product>();

            // Supplier Mapping
            CreateMap<Supplier, SupplierReadDto>();

            CreateMap<CreateSupplierDto, Supplier>();

            CreateMap<UpdateSupplierDto, Supplier>();

            // InventoryLog Mapping
            CreateMap<InventoryLog, InventoryLogReadDto>()
                .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product.Name));

            CreateMap<CreateInventoryLogDto, InventoryLog>();

            CreateMap<UpdateInventoryLogDto, InventoryLog>();

            // Category Mapping

            // Entity → ReadDto
            CreateMap<Category, CategoryReadDto>();

            // CreateDto → Entity
            CreateMap<CategoryCreateDto, Category>();

            // UpdateDto → Entity
            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
