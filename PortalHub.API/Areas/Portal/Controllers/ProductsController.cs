using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PortalHub.Application.Interfaces;
using PortalHub.Application.DTOs.Portal;

namespace PortalHub.Api.Areas.Portal.Controllers
{
    [Area("Portal")]
    [ApiController]
    [Route("api/portal/products")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly ICrudService<
            CreateProductDto,
            UpdateProductDto,
            ProductResponseDto> _service;

        public ProductsController(
            ICrudService<
                CreateProductDto,
                UpdateProductDto,
                ProductResponseDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductDto dto)
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
