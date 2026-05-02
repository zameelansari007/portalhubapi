using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Win32;
using PortalHub.API.Common;
using PortalHub.Application.Common;
using PortalHub.Application.DTOs.Portal;
using PortalHub.Application.Interfaces.Portal;
using PortalHub.Application.Services;
using PortalHub.Domain.Models.Portal;

namespace PortalHub.API.Controllers
{
    [ApiController]
    [Route("api/portal/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _service.GetByIdAsync(id);
            return this.FromServiceResult(result);
        }

        // ---------- REGISTER ----------
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto dto)
        {
            var result = await _service.RegisterAsync(dto);
            return this.FromServiceResult(result);
        }

        // ---------- UPDATE PROFILE ----------
        [HttpPut("{userId:long}")]
        public async Task<IActionResult> Update(long userId, [FromBody] UpdateUserDto dto)
        {
            //var result = await _service.UpdateProfileAsync(userId, dto);

            //if (!result.Success && result.ErrorCode == ErrorCodes.NotFound)
            //    return NotFound(result);

            //return result.Success ? Ok(result) : BadRequest(result);

            var result = await _service.UpdateProfileAsync(userId, dto);

            if (userId != dto.UserId)
            {
                var mismatch = ServiceResult<UserDto>.Fail(
                    "UserId mismatch", ErrorCodes.ValidationError);
                return this.FromServiceResult(mismatch);
            }

            return this.FromServiceResult(result);
        }

        // ---------- VERIFY EMAIL ----------
        //[HttpPost("{userId:long}/verify-email")]
        //public async Task<IActionResult> VerifyEmail(long userId)
        //{
        //    var result = await _service.VerifyEmailAsync(userId);

        //    if (!result.Success && result.ErrorCode == ErrorCodes.NotFound)
        //        return NotFound(result);

        //    return result.Success ? Ok(result) : BadRequest(result);
        //}

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailDto dto)
        {
            var result = await _service.VerifyEmailAsync(dto);
            return this.FromServiceResult(result);
        }

        [HttpPost("verify-mobile")]
        public async Task<IActionResult> VerifyMobile([FromBody] VerifyMobileDto dto)
        {
            var result = await _service.VerifyMobileAsync(dto);
            return this.FromServiceResult(result);
        }

    }
}


//POST / api / portal / users / register
//PUT / api / portal / users /{ userId}
//POST / api / portal / users /{ userId}/ verify - email
