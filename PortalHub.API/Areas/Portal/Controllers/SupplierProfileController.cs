using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalHub.Application.DTOs.Portal;
using PortalHub.Application.Services;

namespace PortalHub.Api.Areas.Portal.Controllers
{
    [Area("Portal")]
    [ApiController]
    [Route("api/portal/supplier-profile")]
    [Authorize]
    public class SupplierProfileController : ControllerBase
    {
       
        private readonly ICrudService<CreateSupplierProfileDto,UpdateSupplierProfileDto,SupplierProfileResponseDto> _service;


        public SupplierProfileController(
        ICrudService<
        CreateSupplierProfileDto,
        UpdateSupplierProfileDto,
        SupplierProfileResponseDto> service)
        {
            _service = service;
        }


        // ---------- READ ----------
        [HttpGet("{supplierId:long}")]
        public async Task<IActionResult> Get(long supplierId)
        {
            var profile = await _service.GetByIdAsync(supplierId);
            return profile == null ? NotFound() : Ok(profile);
        }

        // ---------- CREATE ----------
        [HttpPost]
        public async Task<IActionResult> Create(CreateSupplierProfileDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // ---------- UPDATE ----------
        [HttpPut("{supplierId:long}")]
        public async Task<IActionResult> Update(
            long supplierId,
            UpdateSupplierProfileDto dto)
        {
            if (supplierId != dto.SupplierId)
                return BadRequest("SupplierId mismatch");

            var result = await _service.UpdateAsync(dto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
