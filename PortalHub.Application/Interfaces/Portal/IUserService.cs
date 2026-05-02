using PortalHub.Application.Common;
using PortalHub.Application.DTOs.Portal;

namespace PortalHub.Application.Interfaces.Portal
{
    public interface IUserService
    {
        Task<ServiceResult<UserDto>> RegisterAsync(CreateUserDto dto);
        Task<ServiceResult<UserDto>> UpdateProfileAsync(long userId, UpdateUserDto dto);
        Task<ServiceResult<bool>> VerifyEmailAsync(VerifyEmailDto dto);
        Task<ServiceResult<bool>> VerifyMobileAsync(VerifyMobileDto dto);

        /* READ */

        //Task<UserDto?> GetByIdAsync(long userId);
        Task<ServiceResult<UserDto>> GetByIdAsync(long userId);
    }
}
