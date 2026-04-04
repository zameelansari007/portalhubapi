using AutoMapper;
using PortalHub.Domain.Models.Portal;
using PortalHub.Application.DTOs.Portal;

namespace PortalHub.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponseDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}
