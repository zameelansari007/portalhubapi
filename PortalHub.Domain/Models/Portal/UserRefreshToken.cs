using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PortalHub.Domain.Models.Portal
{
    [Table("UserRefreshTokens", Schema = "Portal")]
    public class UserRefreshToken
    {
        [Key]
        public long RefreshTokenId { get; set; }
        public long UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime? RevokedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ReplacedByToken { get; set; }

        public User User { get; set; }
    }
}
