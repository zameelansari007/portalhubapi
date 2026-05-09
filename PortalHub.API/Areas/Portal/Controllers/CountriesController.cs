using Microsoft.AspNetCore.Mvc;
using PortalHub.API.Common;
using PortalHub.Application.DTOs.Master;
using PortalHub.Application.Interfaces;

namespace PortalHub.Api.Areas.Portal.Controllers
{
    [Area("Portal")]
    [ApiController]
    [Route("api/portal/countries")]
    public class CountriesController : ControllerBase
    {
        private readonly ICrudService<
            CreateCountryDto,
            UpdateCountryDto,
            CountryResponseDto> _service;

        public CountriesController(
            ICrudService<
                CreateCountryDto,
                UpdateCountryDto,
                CountryResponseDto> service)
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
        public async Task<IActionResult> Create(CreateCountryDto dto)
            => this.FromServiceResult(await _service.CreateAsync(dto));

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCountryDto dto)
            => this.FromServiceResult(await _service.UpdateAsync(dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
            => this.FromServiceResult(await _service.DeleteAsync(id));
    }
}