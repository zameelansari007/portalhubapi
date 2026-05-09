using PortalHub.Application.Common;
using PortalHub.Application.DTOs.Auth;

namespace PortalHub.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<ServiceResult<LoginResponseDto>> LoginAsync(LoginRequestDto request,string ipAddress,string userAgent);
        Task<ServiceResult<bool>> ForgotPasswordAsync(ForgotPasswordDto dto);

        Task<ServiceResult<bool>> VerifyResetOtpAsync(VerifyResetOtpDto dto);

        Task<ServiceResult<bool>> ResetPasswordAsync(ResetPasswordDto dto);
    }
}
