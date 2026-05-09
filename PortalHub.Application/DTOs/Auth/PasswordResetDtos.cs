namespace PortalHub.Application.DTOs.Auth
{
    public class ForgotPasswordDto
    {
        public string Email { get; set; } = null!;
    }

    public class VerifyResetOtpDto
    {
        public string Email { get; set; } = null!;
        public string OtpCode { get; set; } = null!;
    }

    public class ResetPasswordDto
    {
        public string Email { get; set; } = null!;
        public string OtpCode { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}