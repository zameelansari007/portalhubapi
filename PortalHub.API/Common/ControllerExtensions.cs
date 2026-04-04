using Microsoft.AspNetCore.Mvc;
using PortalHub.Application.Common;

namespace PortalHub.API.Common
{
    public static class ControllerExtensions
    {
        public static IActionResult FromServiceResult<T>(
            this ControllerBase controller,
            ServiceResult<T> result)
        {
            if (result.Success)
                return controller.Ok(result.Data);

            return result.ErrorCode switch
            {
                ErrorCodes.InvalidCredentials
                    => controller.Unauthorized(new
                    {
                        message = result.Message,
                        errorCode = result.ErrorCode
                    }),

                ErrorCodes.UserInactive
                    => controller.Forbid(),

                _ => controller.BadRequest(new
                {
                    message = result.Message,
                    errorCode = result.ErrorCode
                })
            };
        }
    }
}
