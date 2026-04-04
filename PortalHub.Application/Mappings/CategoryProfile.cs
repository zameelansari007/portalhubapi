using AutoMapper;
using PortalHub.Domain.Models.Portal;
using PortalHub.Application.DTOs.Portal;

namespace PortalHub.Application.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryResponseDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}
