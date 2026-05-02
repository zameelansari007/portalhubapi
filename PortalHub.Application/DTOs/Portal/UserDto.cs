using System;
using System.Collections.Generic;
using System.Text;

namespace PortalHub.Application.DTOs.Portal
{
    public class UserDto
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public string? Phone { get; set; }          // ✅ ADDED
        public bool IsEmailVerified { get; set; }
        public bool IsPhoneVerified { get; set; }
        public bool IsSupplier { get; set; }
    }

    public class UpdateUserDto
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Phone { get; set; }
    }

    public class CreateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string? Phone { get; set; }   // ✅ ADDED
        public int? RoleId { get; set; }
        public bool IsSupplier { get; set; }
    }

    public class VerifyEmailDto
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string OtpCode { get; set; }
    }

    public class VerifyMobileDto
    {
        public long UserId { get; set; }
        public string Mobile { get; set; }
        public string OtpCode { get; set; }
    }

}
