
using Microsoft.AspNetCore.Mvc;
using PortalHub.API.Common;
using PortalHub.Application.DTOs.EnquiryMst;
using PortalHub.Application.Interfaces.Portal;

namespace PortalHub.Api.Areas.Portal.Controllers
{
[Area("Portal")]
[ApiController]
[Route("api/portal/enquiries")]
public class EnquiryController : ControllerBase
{
    private readonly IEnquiryService _service;

    public EnquiryController(IEnquiryService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEnquiryDto dto)
        => this.FromServiceResult(await _service.CreateAsync(dto));

       
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
            => this.FromServiceResult(await _service.GetByIdAsync(id));

    [HttpPut("update-status")]
    public async Task<IActionResult> UpdateStatus(UpdateEnquiryDto dto)
        => this.FromServiceResult(await _service.UpdateStatusAsync(dto));

    [HttpPost("followup")]
    public async Task<IActionResult> Followup(AddEnquiryFollowupDto dto)
        => this.FromServiceResult(await _service.AddFollowupAsync(dto));

    [HttpPost("forward")]
    public async Task<IActionResult> Forward(ForwardEnquiryDto dto)
        => this.FromServiceResult(await _service.ForwardAsync(dto));

        [HttpGet("list-statuses")]
public async Task<IActionResult> GetStatuses()
    => this.FromServiceResult(await _service.GetStatusesAsync());
}

}