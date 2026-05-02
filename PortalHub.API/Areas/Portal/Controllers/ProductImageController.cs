using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalHub.API.Common;
using PortalHub.Application.DTOs.Portal;
using PortalHub.Application.Interfaces;

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
            //return Ok(result);
            return this.FromServiceResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductImageDto dto)
        {
            var result = await _service.CreateAsync(dto);
            //return Ok(result);
            return this.FromServiceResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductImageDto dto)
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
