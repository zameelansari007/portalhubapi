using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalHub.API.Common;
using PortalHub.Application.Common;
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
            var result = await _service.GetByIdAsync(supplierId);
            return this.FromServiceResult(result);
        }

        // ---------- CREATE ----------
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSupplierProfileDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return this.FromServiceResult(result);
        }

        // ---------- UPDATE ----------
        [HttpPut("{supplierId:long}")]
        public async Task<IActionResult> Update( long supplierId, [FromBody] UpdateSupplierProfileDto dto)
        {
            if (supplierId != dto.SupplierId)
            {
                var mismatch = ServiceResult<SupplierProfileResponseDto>.Fail(
                    "SupplierId mismatch", ErrorCodes.ValidationError);
                return this.FromServiceResult(mismatch);
            }

            var result = await _service.UpdateAsync(dto);
            return this.FromServiceResult(result);
        }
    }
}
