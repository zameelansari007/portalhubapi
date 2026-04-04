using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Win32;
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
            return Ok(result);
        }

        // ---------- REGISTER ----------
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserDto dto)
        {
            var result = await _service.RegisterAsync(dto);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // ---------- UPDATE PROFILE ----------
        [HttpPut("{userId:long}")]
        public async Task<IActionResult> Update(long userId, UpdateUserDto dto)
        {
            var result = await _service.UpdateProfileAsync(userId, dto);

            if (!result.Success && result.ErrorCode == ErrorCodes.NotFound)
                return NotFound(result);

            return result.Success ? Ok(result) : BadRequest(result);
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
        public async Task<IActionResult> VerifyEmail(VerifyEmailDto dto)
        {
            var result = await _service.VerifyEmailAsync(dto);

            if (!result.Success && result.ErrorCode == ErrorCodes.NotFound)
                return NotFound(result);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("verify-mobile")]
        public async Task<IActionResult> VerifyMobile(VerifyMobileDto dto)
        {
            var result = await _service.VerifyMobileAsync(dto);

            if (!result.Success && result.ErrorCode == ErrorCodes.NotFound)
                return NotFound(result);

            return result.Success ? Ok(result) : BadRequest(result);
        }

    }
}


//POST / api / portal / users / register
//PUT / api / portal / users /{ userId}
//POST / api / portal / users /{ userId}/ verify - email
