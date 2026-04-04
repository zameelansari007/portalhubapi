using AutoMapper;
using PortalHub.Application.DTOs.Portal;
using PortalHub.Domain.Models.Portal;

namespace PortalHub.Application.Mappings
{
    public class SupplierProfileMapping : Profile
    {
        public SupplierProfileMapping()
        {
            CreateMap<CreateSupplierProfileDto, SupplierProfile>()
                .ForMember(d => d.CreatedAt, o => o.MapFrom(_ => DateTime.UtcNow));

            CreateMap<UpdateSupplierProfileDto, SupplierProfile>()
                .ForMember(d => d.UpdatedAt, o => o.MapFrom(_ => DateTime.UtcNow));

            CreateMap<SupplierProfile, SupplierProfileResponseDto>();
        }
    }
}
