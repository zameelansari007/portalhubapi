using AutoMapper;
using PortalHub.Application.DTOs.EnquiryMst;
using PortalHub.Domain.Models.EnquiryMst;

namespace PortalHub.Application.Mappings
{
public class EnquiryFollowupProfile : Profile
{
    public EnquiryFollowupProfile()
    {
        CreateMap<AddEnquiryFollowupDto, EnquiryFollowup>()
            .ForMember(d => d.CreatedAt,
                o => o.MapFrom(_ => DateTime.UtcNow));

        CreateMap<EnquiryFollowup, EnquiryFollowupDto>();
    }
}
}