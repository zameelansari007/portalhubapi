using Microsoft.AspNetCore.Http;
using PortalHub.Application.Interfaces.Auth;

public class IpBlockMiddleware
{
    private readonly RequestDelegate _next;

    public IpBlockMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IAuthRepository repo)
    {
        if (context.Request.Path.StartsWithSegments("/api/portal/auth/login"))
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                     ?? context.Connection.RemoteIpAddress?.ToString();

            if (!string.IsNullOrWhiteSpace(ip))
            {
                var blocked = await repo.IsIpBlockedAsync(ip);

                if (blocked)
                {
                    context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(
                        "{\"error\":\"Too many login attempts. Try again later.\"}"
                    );

                    return;
                }
            }
        }

        await _next(context);
    }
}