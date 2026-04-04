namespace PortalHub.Application.Common
{
    public static class ErrorCodes
    {
        public const string NotFound = "GEN_404";
        public const string ValidationError = "GEN_400";
        public const string ServerError = "GEN_500";

        public const string InvalidCredentials = "AUTH_001";
        public const string UserInactive = "AUTH_002";
        public const string InvalidToken = "AUTH_003";
        public const string InvalidOTP = "AUTH_004";
        public const string ExpiredOTP = "AUTH_005";
        public const string InvalidEmailToken = "AUTH_006";
        public const string TooManyAttemptOTP = "AUTH_007";

        public const string EmailAlreadyExists = "Cust_001";
        public const string MobileAlreadyExists = "Cust_002";
        public const string EmailNotVerified = "Cust_003";
        public const string MobileNotVerified = "Cust_004";
    }

public static class AuthAuditEvents
{
    public const string LoginSuccess = "LOGIN_SUCCESS";
    public const string LoginFailed = "LOGIN_FAILED";
    public const string AccountLocked = "ACCOUNT_LOCKED";
    public const string AccountInactive = "ACCOUNT_INACTIVE";
    public const string Logout = "LOGOUT";
    public const string TokenRefresh = "TOKEN_REFRESH";
        public const string IpBlocked = "IP_BLOCKED";
    }
}
