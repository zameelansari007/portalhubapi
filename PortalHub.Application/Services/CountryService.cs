using AutoMapper;
using FluentValidation;
using PortalHub.Application.DTOs.Master;
using PortalHub.Application.Interfaces.Queries;
using PortalHub.Application.Interfaces.Repositories;
using PortalHub.Domain.Models.Master;

namespace PortalHub.Application.Services
{
    public class CountryService :
        CrudService<
            Country,
            CreateCountryDto,
            UpdateCountryDto,
            CountryResponseDto>
    {
        public CountryService(
            IQueryRepository<Country> queryRepo,
            IRepository<Country> commandRepo,
            IMapper mapper,
            IValidator<Country> validator)
            : base(queryRepo, commandRepo, mapper, validator)
        {
        }
    }
}