using Microsoft.AspNetCore.Mvc;
using PortalHub.API.Common;
using PortalHub.Application.DTOs.Master;
using PortalHub.Application.Interfaces;

namespace PortalHub.Api.Areas.Portal.Controllers
{
    [Area("Portal")]
    [ApiController]
    [Route("api/portal/states")]
    public class StatesController : ControllerBase
    {
        private readonly ICrudService<
            CreateStateDto,
            UpdateStateDto,
            StateResponseDto> _service;

        public StatesController(
            ICrudService<
                CreateStateDto,
                UpdateStateDto,
                StateResponseDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => this.FromServiceResult(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
            => this.FromServiceResult(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateStateDto dto)
            => this.FromServiceResult(await _service.CreateAsync(dto));

        [HttpPut]
        public async Task<IActionResult> Update(UpdateStateDto dto)
            => this.FromServiceResult(await _service.UpdateAsync(dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
            => this.FromServiceResult(await _service.DeleteAsync(id));
    }
}