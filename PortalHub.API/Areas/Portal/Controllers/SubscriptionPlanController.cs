using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalHub.Application.DTOs.Master;

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
            return Ok(plans);
        }

        // ==============================
        // GET BY ID
        // ==============================
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var plan = await _service.GetByIdAsync(id);

            if (plan == null)
                return NotFound();

            return Ok(plan);
        }

        // ==============================
        // CREATE
        // ==============================
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateSubscriptionPlanDto dto)
        {
            var result = await _service.CreateAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        // ==============================
        // UPDATE
        // ==============================
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateSubscriptionPlanDto dto)
        {
            if (id != dto.PlanId)
                return BadRequest("PlanId mismatch");

            var result = await _service.UpdateAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        // ==============================
        // DELETE
        // ==============================
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.Success)
            {
                if (result.ErrorCode == "PLAN_ACTIVE")
                    return Conflict(result); // better than BadRequest

                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
