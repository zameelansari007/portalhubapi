using Microsoft.AspNetCore.Mvc;
using PortalHub.Application.Common;

namespace PortalHub.API.Common
{
    //public static class ControllerExtensions
    //{
    //    public static IActionResult FromServiceResult<T>(
    //        this ControllerBase controller,
    //        ServiceResult<T> result)
    //    {
    //        if (result.Success)
    //            return controller.Ok(result.Data);

    //        return result.ErrorCode switch
    //        {
    //            ErrorCodes.InvalidCredentials
    //                => controller.Unauthorized(new
    //                {
    //                    message = result.Message,
    //                    errorCode = result.ErrorCode
    //                }),

    //            ErrorCodes.UserInactive
    //                => controller.Forbid(),

    //            _ => controller.BadRequest(new
    //            {
    //                message = result.Message,
    //                errorCode = result.ErrorCode
    //            })
    //        };
    //    }
    //}

    public static class ControllerExtensions
    {
        public static IActionResult FromServiceResult<T>(
            this ControllerBase controller,
            ServiceResult<T> result)
        {
            var response = new ApiResponse<T>
            {
                Success = result.Success,
                Message = result.Message,
                Data = result.Success ? result.Data : default,
                Error = result.Success ? null : result.ErrorCode
            };

            return result.Success
                ? controller.Ok(response)
                : result.ErrorCode switch
                {
                    // Generic
                    ErrorCodes.NotFound => controller.NotFound(response),
                    ErrorCodes.ValidationError => controller.BadRequest(response),
                    ErrorCodes.ServerError => controller.StatusCode(StatusCodes.Status500InternalServerError, response),

                    // Auth
                    ErrorCodes.InvalidCredentials => controller.Unauthorized(response),
                    ErrorCodes.UserInactive => controller.Forbid(),
                    ErrorCodes.InvalidToken => controller.Unauthorized(response),
                    ErrorCodes.InvalidOTP => controller.BadRequest(response),
                    ErrorCodes.ExpiredOTP => controller.BadRequest(response),
                    ErrorCodes.InvalidEmailToken => controller.BadRequest(response),
                    ErrorCodes.TooManyAttemptOTP => controller.StatusCode(StatusCodes.Status429TooManyRequests, response),

                    // Customer
                    ErrorCodes.EmailAlreadyExists => controller.Conflict(response),
                    ErrorCodes.MobileAlreadyExists => controller.Conflict(response),
                    ErrorCodes.EmailNotVerified => controller.BadRequest(response),
                    ErrorCodes.MobileNotVerified => controller.BadRequest(response),

                   
                    "PLAN_ACTIVE" => controller.Conflict(response), // domain-specific
                    _ => controller.BadRequest(response)
                };
        }
    }


}
