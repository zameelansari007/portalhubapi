using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PortalHub.Application.Interfaces;
using PortalHub.Application.DTOs.Portal;

namespace PortalHub.Api.Areas.Portal.Controllers
{
    [Area("Portal")]
    [ApiController]
    [Route("api/portal/productimages")]
    [Authorize]
    public class ProductsImageController : ControllerBase
    {
        private readonly ICrudService<
            CreateProductImageDto,
            UpdateProductImageDto,
            ProductImageResponseDto> _service;

        public ProductsImageController(
            ICrudService<
                CreateProductImageDto,
                UpdateProductImageDto,
                ProductImageResponseDto> service)
        {
            _service = service;
        }

        

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductImageDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductImageDto dto)
        {
            var result = await _service.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok(result);
        }
    }
}
