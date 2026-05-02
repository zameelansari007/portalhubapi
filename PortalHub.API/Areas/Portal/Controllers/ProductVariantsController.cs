using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalHub.API.Common;
using PortalHub.Application.DTOs.Portal;
using PortalHub.Application.Interfaces;

namespace PortalHub.Api.Areas.Portal.Controllers
{
    [Area("Portal")]
    [ApiController]
    [Route("api/portal/product-variants")]
    [Authorize]
    public class ProductVariantsController : ControllerBase
    {
        private readonly ICrudService<
            CreateProductVariantDto,
            UpdateProductVariantDto,
            ProductVariantResponseDto> _service;

        public ProductVariantsController(
            ICrudService<
                CreateProductVariantDto,
                UpdateProductVariantDto,
                ProductVariantResponseDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            //return Ok(result);
            return this.FromServiceResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _service.GetByIdAsync(id);
            //return Ok(result);
            return this.FromServiceResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductVariantDto dto)
        {
            var result = await _service.CreateAsync(dto);
            //return Ok(result);
            return this.FromServiceResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductVariantDto dto)
        {
            var result = await _service.UpdateAsync(dto);
            //return Ok(result);
            return this.FromServiceResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _service.DeleteAsync(id);
            //return Ok(result);
            return this.FromServiceResult(result);
        }
    }
}
