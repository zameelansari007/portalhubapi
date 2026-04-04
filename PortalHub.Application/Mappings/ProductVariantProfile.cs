using AutoMapper;
using PortalHub.Domain.Models.Portal;
using PortalHub.Application.DTOs.Portal;

public class ProductVariantProfile : Profile
{
    public ProductVariantProfile()
    {
        CreateMap<ProductVariant, ProductVariantResponseDto>();
        CreateMap<CreateProductVariantDto, ProductVariant>();
        CreateMap<UpdateProductVariantDto, ProductVariant>();
    }
}
