using AutoMapper;
using PortalHub.Application.DTOs.Master;
using PortalHub.Domain.Models.Master;

namespace PortalHub.Application.Mappings
{
    public class StateProfile : Profile
    {
        public StateProfile()
        {
            CreateMap<CreateStateDto, State>();
            CreateMap<UpdateStateDto, State>();
            CreateMap<State, StateResponseDto>();
        }
    }
}
