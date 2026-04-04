using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;


namespace PortalHub.Domain.Models.Portal
{
    [Table("UserOtps", Schema = "Portal")]
    public class UserOtp
    {
        [Key]
        public long OtpId { get; set; }
        public long UserId { get; set; }
        public string OtpCode { get; set; }
        
        public bool IsUsed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public DateTime ExpireAt { get; set; }
        public OtpType OtpType { get; set; }
        public int AttemptCount { get; set; }
        public User User { get; set; }
    }

    public enum OtpType
    {
        EmailVerification=1,
        MobileVerification=2,
        PasswordReset=3
    }

    


}
