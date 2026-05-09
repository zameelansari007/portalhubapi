using AutoMapper;
using PortalHub.Application.DTOs.Master;
using PortalHub.Domain.Models.Master;

namespace PortalHub.Application.Mappings
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<CreateCountryDto, Country>();
            CreateMap<UpdateCountryDto, Country>();
            CreateMap<Country, CountryResponseDto>();
        }
    }
}
