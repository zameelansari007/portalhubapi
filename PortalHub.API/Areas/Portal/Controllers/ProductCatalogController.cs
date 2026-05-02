using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalHub.API.Common;
using PortalHub.Application.DTOs.Catalog;
using PortalHub.Application.DTOs.Portal;
using PortalHub.Application.Interfaces.Queries;


namespace PortalHub.API.Areas.Portal.Controllers
{
    [Area("Portal")]
    [ApiController]
    [Route("api/portal/catalog")]
    [Authorize]
    public class ProductCatalogController: ControllerBase
    {
        private readonly ICatalogQueryRepository _repo;

        public ProductCatalogController(ICatalogQueryRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories(long supplierId)
        {
            var result = await _repo.GetCategoriesAsync(supplierId);

            //return Ok(result);
            return this.FromServiceResult(result);
        }

        [HttpPost("products")]
        public async Task<IActionResult> GetProducts([FromBody] ProductListRequestDto request)
        {
            var result = await _repo.GetProductsAsync(request);

            // return Ok(result);
            return this.FromServiceResult(result);
        }


    }
}
