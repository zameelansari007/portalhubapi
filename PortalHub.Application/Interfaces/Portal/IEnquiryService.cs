using PortalHub.Application.Common;
using PortalHub.Application.DTOs.EnquiryMst;

namespace PortalHub.Application.Interfaces.Portal
{
    public interface IEnquiryService
{
    Task<ServiceResult<long>> CreateAsync(CreateEnquiryDto dto);
    Task<ServiceResult<bool>> UpdateStatusAsync(UpdateEnquiryDto dto);

    Task<ServiceResult<bool>> AddFollowupAsync(AddEnquiryFollowupDto dto);

    Task<ServiceResult<bool>> ForwardAsync(ForwardEnquiryDto dto);

    Task<ServiceResult<EnquiryDto>> GetByIdAsync(long id);
     Task<ServiceResult<IEnumerable<EnquiryStatusDto>>> GetStatusesAsync();
}
}
