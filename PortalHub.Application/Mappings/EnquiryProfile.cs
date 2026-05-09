using AutoMapper;
using PortalHub.Application.DTOs.EnquiryMst;
using PortalHub.Domain.Models.EnquiryMst;

namespace PortalHub.Application.Mappings
{
public class EnquiryProfile : Profile
{
    public EnquiryProfile()
    {
        // CREATE
        CreateMap<CreateEnquiryDto, Enquiry>()
            .ForMember(d => d.CreatedAt,
                o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.StatusId,
                o => o.MapFrom(_ => 1)); // New

        // UPDATE STATUS / ASSIGN
        CreateMap<UpdateEnquiryDto, Enquiry>();

        // DOMAIN → DTO (MAIN FIX)
        CreateMap<Enquiry, EnquiryDto>()
            .ForMember(d => d.StatusName,
                o => o.Ignore()) // will be handled in query or join
            .ForMember(d => d.ProductName,
                o => o.Ignore());

              CreateMap<EnquiryStatus, EnquiryStatusDto>();
    }
}
}