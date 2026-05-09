using AutoMapper;
using PortalHub.Application.DTOs.Master;
using PortalHub.Domain.Models.Master;

namespace PortalHub.Application.Mappings
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<CreateCityDto, City>();
            CreateMap<UpdateCityDto, City>();
            CreateMap<City, CityResponseDto>();
        }
    }
}
