using Microsoft.AspNetCore.Mvc;
using PortalHub.Application.Services;
using PortalHub.Domain.Models.Portal;

namespace PortalHub.Api.Areas.Portal.Controllers;

[Area("Portal")]
[Route("api/[area]/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }
}
