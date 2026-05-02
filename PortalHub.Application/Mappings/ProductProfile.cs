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
            //CreateMap<Product, ProductResponseDto>()
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId.ToString()))
            //.ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl)))
            //.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
            //.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Variants.FirstOrDefault().Price))
            //.ForMember(dest => dest.OriginalPrice, opt => opt.MapFrom(src => src.Variants.FirstOrDefault().CompareAtPrice))
            //.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.CategoryId))
            //.ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Variants.Sum(v => v.StockQuantity)))
            //.ForMember(dest => dest.Sold, opt => opt.Ignore())       // you’ll calculate from orders
            //.ForMember(dest => dest.Rating, opt => opt.Ignore())     // you’ll calculate from reviews
            //.ForMember(dest => dest.Delivery, opt => opt.Ignore())   // business logic
            //.ForMember(dest => dest.VideoAvailable, opt => opt.Ignore());
        }
    }
}
