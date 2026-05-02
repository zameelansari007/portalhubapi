using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalHub.API.Common;
using PortalHub.Application.Common;
using PortalHub.Application.DTOs.Master;
using System.Numerics;

namespace PortalHub.Api.Areas.Master.Controllers
{
    [Area("Master")]
    [ApiController]
    [Route("api/master/subscription-plans")]
    [Authorize] // remove if public
    public class SubscriptionPlanController : ControllerBase
    {
        private readonly SubscriptionPlanService _service;

        public SubscriptionPlanController(
            SubscriptionPlanService service)
        {
            _service = service;
        }

        // ==============================
        // GET ALL
        // ==============================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var plans = await _service.GetAllAsync();
            //return Ok(plans);
            return this.FromServiceResult(plans);
        }

        // ==============================
        // GET BY ID
        // ==============================
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var plan = await _service.GetByIdAsync(id);

            //if (plan == null)
            //    return NotFound();

            //return Ok(plans);
            return this.FromServiceResult(plan);
        }

        // ==============================
        // CREATE
        // ==============================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubscriptionPlanDto dto)
        {
            var result = await _service.CreateAsync(dto);

            //if (!result.Success)
            //    return BadRequest(result);

            //return Ok(result);
            return this.FromServiceResult(result);
        }

        // ==============================
        // UPDATE
        // ==============================
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update( int id,[FromBody] UpdateSubscriptionPlanDto dto)
        {
            if (id != dto.PlanId)
            {
                var mismatch = ServiceResult<SubscriptionPlanDto>.Fail(
                    "PlanId mismatch", ErrorCodes.ValidationError);
                return this.FromServiceResult(mismatch);
            }

            var result = await _service.UpdateAsync(dto);
            return this.FromServiceResult(result);
        }

        // ==============================
        // DELETE
        // ==============================
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return this.FromServiceResult(result);
        }
    }
}
