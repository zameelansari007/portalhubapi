namespace PortalHub.Application.Common
{
    public class ServiceResult<T>
    {
        public bool Success { get; private set; }
        public string Message { get; private set; } = string.Empty;
        public string? ErrorCode { get; private set; }
        public T? Data { get; private set; }

        public static ServiceResult<T> Ok(T data, string message = "Success")
            => new ServiceResult<T> { Success = true, Data = data, Message = message };

        public static ServiceResult<T> Fail(string message, string errorCode)
            => new ServiceResult<T> { Success = false, Message = message, ErrorCode = errorCode };
    }
}
