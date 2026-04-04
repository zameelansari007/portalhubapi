using PortalHub.Domain.Models.Portal;

namespace PortalHub.Application.Interfaces.Auth
{
    public interface IJwtTokenGenerator
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
    }
}
