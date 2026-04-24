using AutoMapper;
using SmartInventory.Application.DTOs.ProductsDtos;
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
        }
    }
}
