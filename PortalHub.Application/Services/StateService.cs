using AutoMapper;
using FluentValidation;
using PortalHub.Application.DTOs.Master;
using PortalHub.Application.Interfaces.Queries;
using PortalHub.Application.Interfaces.Repositories;
using PortalHub.Domain.Models.Master;

namespace PortalHub.Application.Services
{
    public class StateService :
        CrudService<
            State,
            CreateStateDto,
            UpdateStateDto,
            StateResponseDto>
    {
        public StateService(
            IQueryRepository<State> queryRepo,
            IRepository<State> commandRepo,
            IMapper mapper,
            IValidator<State> validator)
            : base(queryRepo, commandRepo, mapper, validator)
        {
        }
    }
}