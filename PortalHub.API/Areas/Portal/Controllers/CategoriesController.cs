using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalHub.API.Common;
using PortalHub.Application.DTOs.Portal;
using PortalHub.Application.Interfaces;

namespace PortalHub.Api.Areas.Portal.Controllers
{
    [Area("Portal")]
    [ApiController]
    [Route("api/portal/categories")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICrudService<
            CreateCategoryDto,
            UpdateCategoryDto,
            CategoryResponseDto> _service;

        public CategoriesController(
            ICrudService<
                CreateCategoryDto,
                UpdateCategoryDto,
                CategoryResponseDto> service)
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
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            var result = await _service.CreateAsync(dto);
            //return Ok(result);
            return this.FromServiceResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryDto dto)
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
