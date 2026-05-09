using AutoMapper;
using PortalHub.Application.DTOs.EnquiryMst;
using PortalHub.Domain.Models.EnquiryMst;

namespace PortalHub.Application.Mappings
{

public class EnquiryForwardProfile : Profile
{
    public EnquiryForwardProfile()
    {
        CreateMap<ForwardEnquiryDto, EnquiryForward>()
            .ForMember(d => d.ForwardedAt,
                o => o.MapFrom(_ => DateTime.UtcNow));
    }
}
}