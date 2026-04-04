using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PortalHub.Domain.Models.Master;

namespace PortalHub.Domain.Models.Portal
{
    [Table("Users", Schema = "Portal")]
    public class User
    {
        [Key]
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsEmailVerified { get; set; }
        public DateTime? EmailVerifiedAt { get; set; }
        public string? Phone { get; set; }
        public bool IsPhoneVerified { get; set; }
        public DateTime? PhoneVerifiedAt { get; set; }
        public int RoleId { get; set; }
        public bool IsSupplier { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        // 🔒 Security fields
        public int FailedLoginAttempts { get; set; }

        public bool IsLocked { get; set; }

        public DateTime? LockoutEnd { get; set; }

        public DateTime? LastFailedLogin { get; set; }

        public Role Role { get; set; }
        public UserPassword Password { get; set; }
        public SupplierProfile? SupplierProfile { get; set; }
    }

    
}
