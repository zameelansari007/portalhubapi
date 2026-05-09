using AutoMapper;
using FluentValidation;
using PortalHub.Application.DTOs.Master;
using PortalHub.Application.Interfaces.Queries;
using PortalHub.Application.Interfaces.Repositories;
using PortalHub.Domain.Models.Master;

namespace PortalHub.Application.Services
{
    public class CityService :
        CrudService<
            City,
            CreateCityDto,
            UpdateCityDto,
            CityResponseDto>
    {
        public CityService(
            IQueryRepository<City> queryRepo,
            IRepository<City> commandRepo,
            IMapper mapper,
            IValidator<City> validator)
            : base(queryRepo, commandRepo, mapper, validator)
        {
        }
    }
}