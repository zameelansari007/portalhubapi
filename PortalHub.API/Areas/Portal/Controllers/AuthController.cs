using Microsoft.AspNetCore.Mvc;
using PortalHub.Application.DTOs.Auth;
using PortalHub.Application.Interfaces.Auth;
using PortalHub.API.Common;

[Area("Portal")]
[ApiController]
[Route("api/portal/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    

    public AuthController(IAuthService auth)
    {
        _auth = auth;
        
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var ip = Request.Headers["X-Forwarded-For"].FirstOrDefault()
                 ?? HttpContext.Connection.RemoteIpAddress?.ToString()
                 ?? "Unknown";

        var userAgent = Request.Headers["User-Agent"].ToString();

        if (string.IsNullOrWhiteSpace(userAgent))
            userAgent = "Unknown";

        var result = await _auth.LoginAsync(request, ip, userAgent);

        return this.FromServiceResult(result);
    }
}
